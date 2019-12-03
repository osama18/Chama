using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.ApplicatoionServices.SubScribtionsServices.Dtos
{
    public class CourseSubscribeResponse 
    {
        public bool Subscribed { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorReason ErrorReason { get; set; }

    }
    public enum ErrorReason { 
        CourseComplete,
        TechnicalError
    }
}
