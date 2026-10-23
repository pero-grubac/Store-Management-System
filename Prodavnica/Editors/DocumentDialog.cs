using Prodavnica.App;
using Prodavnica.Database.DTO;
using Prodavnica.UI;
using Prodavnica.UI.Controls;
using System.Drawing.Printing;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Editors
{
    public enum DocumentKind
    {
        Bill,
        Procurement
    }

    // The lines of a bill or a procurement, with printing.
    public class DocumentDialog : DialogForm
    {
        private readonly DocumentKind kind;
        private readonly int id;
        private readonly DataTable table = new DataTable { Dock = DockStyle.Fill };
        private readonly UiLabel totalLabel = new UiLabel(string.Empty, 18f, semibold: true);
        private readonly UiLabel totalCaption = new UiLabel(T.Get("pos.total"), 10.5f, color: Theme.Muted);
        private string person = string.Empty;
        private DateTime date;
        private decimal total;
        private List<(string Name, decimal Amount, decimal Price)> lines = new();

        public DocumentDialog(DocumentKind kind, int id, bool afterSale = false)
            : base(T.Format(kind == DocumentKind.Bill ? "documents.billTitle" : "documents.procurementTitle", T.DocumentNumber(id)), " ", 720)
        {
            this.kind = kind;
            this.id = id;
            table.AddText(T.Get("products.product"), 2.4f);
            table.AddText(T.Get("documents.quantity"), 0.8f, right: true);
            table.AddText(T.Get("documents.unitPrice"), 1f, right: true);
            DataGridViewColumn sum = table.AddText(T.Get("documents.amount"), 1f, right: true);
            sum.DefaultCellStyle.Font = Theme.Semibold(10f);
            var summary = new Panel { Dock = DockStyle.Bottom, Height = LogicalToDeviceUnits(64), BackColor = Theme.Surface };
            summary.Controls.Add(totalCaption);
            summary.Controls.Add(totalLabel);
            summary.Layout += (_, _) =>
            {
                totalLabel.Location = new Point(summary.Width - totalLabel.PreferredWidth, LogicalToDeviceUnits(16));
                totalCaption.Location = new Point(totalLabel.Left - totalCaption.PreferredWidth - LogicalToDeviceUnits(12), LogicalToDeviceUnits(26));
            };
            summary.Paint += (_, e) => { using var pen = new Pen(Theme.Border); e.Graphics.DrawLine(pen, 0, 0, summary.Width, 0); };
            var frame = new Card { Dock = DockStyle.Fill, Padding = new Padding(LogicalToDeviceUnits(8), LogicalToDeviceUnits(4), LogicalToDeviceUnits(16), LogicalToDeviceUnits(4)) };
            frame.Controls.Add(table);
            frame.Controls.Add(summary);
            Body.Controls.Add(frame);

            AcceptButton = AddButton(T.Get(afterSale ? "pos.newSale" : "common.close"), ButtonKind.Primary, result: DialogResult.OK);
            var print = AddButton(T.Get("documents.print"), ButtonKind.Secondary, Glyphs.Print);
            print.Click += (_, _) => Print();
            SetBodyHeight(440);
            Load += async (_, _) => await UI.Dialogs.RunAsync(this, LoadAsync);
        }

        private async Task LoadAsync()
        {
            if (kind == DocumentKind.Bill)
            {
                Bill? bill = await Session.Sales.GetBillAsync(id);
                List<BillItem> items = await Session.Sales.GetItemsAsync(id);
                (person, date, total) = (bill?.CashierName ?? string.Empty, bill?.Date ?? DateTime.Now, bill?.Total ?? 0);
                lines = items.Select(i => (i.Name, i.Amount, i.Price)).ToList();
            }
            else
            {
                Procurement? procurement = await Session.Procurements.GetAsync(id);
                List<ProcurementItem> items = await Session.Procurements.GetItemsAsync(id);
                (person, date, total) = (procurement?.SupplierName ?? string.Empty, procurement?.Date ?? DateTime.Now, procurement?.Total ?? 0);
                lines = items.Select(i => (i.Name, i.Amount, i.Price)).ToList();
            }
            table.SetRows(lines.Select((l, index) => (l, index)), x => new object?[] { x.l.Name, T.Number(x.l.Amount), T.Money(x.l.Price), T.Money(x.l.Amount * x.l.Price) });
            totalLabel.Text = T.Money(total);
            totalLabel.Parent?.PerformLayout();
            string who = T.Get(kind == DocumentKind.Bill ? "documents.cashier" : "documents.supplier");
            SetSubtitle($"{T.DateTime(date)}  ·  {who}: {(person.Length > 0 ? person : "—")}  ·  {T.Format("pos.items", lines.Count)}");
        }

        // A receipt-style printout: store name, document, lines and total.
        private void Print()
        {
            using var document = new PrintDocument { DocumentName = Text };
            document.PrintPage += (_, e) =>
            {
                Graphics g = e.Graphics!;
                using var title = new Font("Segoe UI Semibold", 14);
                using var regular = new Font("Segoe UI", 9.5f);
                using var bold = new Font("Segoe UI Semibold", 9.5f);
                using var big = new Font("Segoe UI Semibold", 13);
                float width = Math.Min(e.MarginBounds.Width, 320);
                float left = e.MarginBounds.Left + (e.MarginBounds.Width - width) / 2, y = e.MarginBounds.Top;
                var center = new StringFormat { Alignment = StringAlignment.Center };
                var right = new StringFormat { Alignment = StringAlignment.Far };
                g.DrawString(T.Get("app.name"), title, Brushes.Black, new RectangleF(left, y, width, 30), center); y += 32;
                g.DrawString(T.Get(kind == DocumentKind.Bill ? "receipt.bill" : "receipt.procurement") + " " + T.DocumentNumber(id), bold, Brushes.Black, new RectangleF(left, y, width, 20), center); y += 22;
                g.DrawString(T.DateTime(date), regular, Brushes.Black, new RectangleF(left, y, width, 20), center); y += 20;
                g.DrawString(person, regular, Brushes.Black, new RectangleF(left, y, width, 20), center); y += 30;
                using var dashed = new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                g.DrawLine(dashed, left, y, left + width, y); y += 10;
                foreach (var (name, amount, price) in lines)
                {
                    g.DrawString(name, bold, Brushes.Black, new RectangleF(left, y, width, 18)); y += 18;
                    g.DrawString($"{T.Number(amount)} x {T.Money(price)}", regular, Brushes.Black, left + 8, y);
                    g.DrawString(T.Money(amount * price), regular, Brushes.Black, new RectangleF(left, y, width, 18), right); y += 22;
                }
                g.DrawLine(dashed, left, y, left + width, y); y += 12;
                g.DrawString(T.Get("pos.total"), big, Brushes.Black, left, y);
                g.DrawString(T.Money(total), big, Brushes.Black, new RectangleF(left, y, width, 26), right); y += 44;
                if (kind == DocumentKind.Bill) g.DrawString(T.Get("receipt.thanks"), regular, Brushes.Black, new RectangleF(left, y, width, 20), center);
            };
            using var preview = new PrintPreviewDialog { Document = document, Width = LogicalToDeviceUnits(760), Height = LogicalToDeviceUnits(860), UseAntiAlias = true };
            preview.ShowDialog(this);
        }
    }
}
