using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Reactive.Concurrency;
using System.Reactive.Linq;


namespace MainAplikasi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // buat dulu Observables by event, untuk ini akan membuat observable dari event mouse press up dan down juga mouse move
            var mouseDownEvents = Observable.FromEventPattern<MouseEventArgs>(this, "MouseLeftButtonDown");
            var mouseMoveEvents = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove");
            var mouseUpEvents = Observable.FromEventPattern<MouseEventArgs>(this, "MouseLeftButtonUp");

            var dragEvents =
                from mouseDownPosition in mouseDownEvents
                from mouseMovePosition in mouseMoveEvents.StartWith(mouseDownPosition)
                                                         .TakeUntil(mouseUpEvents)
                select mouseMovePosition;

            dragEvents.Subscribe(eventPattern =>
            {
                // Move things on UI.
                Console.WriteLine(eventPattern.EventArgs.GetPosition(this));
            });

            //mouseMoveObservableEvent.Subscribe(args => Debug.WriteLine(args.EventArgs.GetPosition(this)));
        }
    }
}