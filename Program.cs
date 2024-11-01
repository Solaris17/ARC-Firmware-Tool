namespace ARC_Firmware_Tool
{
    internal static class Program
    {
        private static Mutex mutex;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isNewInstance;

            // Create a named mutex to prevent multiple instances
            mutex = new Mutex(true, "ARC_Firmware_Tool_f20329d9-7084-457e-bca2-bafaf422f06a", out isNewInstance);

            if (isNewInstance)
            {
                // If no other instance is running, initialize and run the application
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
            else
            {
                // Show a message if another instance is already running
                MessageBox.Show("ARC Firmware Tool is already running!", "Multiple Instances Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
