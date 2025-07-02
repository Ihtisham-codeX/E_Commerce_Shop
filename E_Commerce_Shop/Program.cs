using E_Commerce_Shop.UI;
using E_Commerce_Shop.UI.Admin;
using E_Commerce_Shop.UI.Customer;

namespace E_Commerce_Shop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new HomePage());
        }
    }
}