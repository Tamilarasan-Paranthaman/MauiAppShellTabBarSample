using Microsoft.Maui.Layouts;

namespace MauiApp1
{
    class CustomControl : ControlLayout
    {
        Label label;
        ScrollView scrollView;

        public CustomControl()
        {
            label = new Label();
            label.Text = "Hello";
            label.BackgroundColor = Colors.Red;
            scrollView = new ScrollView();
            scrollView.Content = label;
            scrollView.Scrolled += ScrollView_Scrolled;
            this.Add(scrollView);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Scroll view is scrolled");
            (this as IView).InvalidateMeasure();
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return new ControlLayoutManager(this);
        }

        internal override Size LayoutArrangeChildren(Rect bounds)
        {
            System.Diagnostics.Debug.WriteLine("Scroll view is arranged");
            (this.scrollView as IView).Arrange(bounds);
            return bounds.Size;
        }

        internal override Size LayoutMeasure(double widthConstraint, double heightConstraint)
        {
            System.Diagnostics.Debug.WriteLine("LayoutMeasure called");
            this.label.HeightRequest = 1000;
            (this.scrollView as IView).Measure(300, 300);
            return new Size(300, 300);
        }
    }

    public abstract class ControlLayout : Layout
    {
        internal abstract Size LayoutArrangeChildren(Rect bounds);

        internal abstract Size LayoutMeasure(double widthConstraint, double heightConstraint);
    }

    internal class ControlLayoutManager : LayoutManager
    {
        ControlLayout layout;
        internal ControlLayoutManager(ControlLayout layout) : base(layout)
        {
            this.layout = layout;
        }

        public override Size ArrangeChildren(Rect bounds) => this.layout.LayoutArrangeChildren(bounds);

        public override Size Measure(double widthConstraint, double heightConstraint) => this.layout.LayoutMeasure(widthConstraint, heightConstraint);
    }
}
