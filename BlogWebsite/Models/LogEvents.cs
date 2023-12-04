namespace BlogWebsite.Models
{
    public class LogEvents
    {
        public static void LogFile(string Title, string LogMessage,IWebHostEnvironment eventFile)
        {
            bool exists = Directory.Exists(eventFile.WebRootPath + "\\" + "LogFolder");
            if(!exists) {
                Directory.CreateDirectory(eventFile.WebRootPath + "\\" + "LogFolder");
            }
            StreamWriter writeLogs;
            string logPath;
            string fileName = DateTime.Now.ToString("ddmmyyyy")+".txt";
            logPath = Path.Combine(eventFile.WebRootPath + "\\" + "LogFolder" + fileName);
            if(!File.Exists(logPath))
            {
                writeLogs=new StreamWriter(logPath);
            }
            else
            {
                writeLogs=File.AppendText(logPath);
            }
            writeLogs.WriteLine("Log Entry");
            writeLogs.WriteLine("{0} {1}",DateTime.Now.ToLongDateString(),DateTime.Now.ToLongTimeString());
            writeLogs.WriteLine(":");
            writeLogs.WriteLine("Title : {0} ",Title);
            writeLogs.WriteLine("Message :{1} ",LogMessage);
            writeLogs.WriteLine("--------------------------------------------------------");
            writeLogs.WriteLine();
            writeLogs.Close();
        }
    }
}
