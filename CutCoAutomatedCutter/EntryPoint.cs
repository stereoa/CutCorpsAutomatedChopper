using System.Windows.Input;
namespace CutCoAutomatedCutter
{
   

    public class EntryPoint
    {
        [System.STAThreadAttribute]
        static void Main()
        {
            using (var hook = new KeyboardHook())
            {
                var app = new App(hook);
                app.Run();
            }
        }

    }
}