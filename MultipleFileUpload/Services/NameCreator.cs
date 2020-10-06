using MultipleFileUpload.Interfaces;
using System;

namespace MultipleFileUpload.Services
{
    public class NameCreator : INameCreator
    {
        public string Create()
        {
            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string minutes = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();
            string milisecond = DateTime.Now.Millisecond.ToString();
            return day + month + year + hour + minutes + second + milisecond;
        }
    }
}
