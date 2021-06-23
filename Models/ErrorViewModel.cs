using System;

namespace PizzeriaOnline.Models
{   
    public class ErrorViewModel
    {
        /*! Klasa błedu */
        public string RequestId { get; set; } /*! identyfikator żądania */

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); /*! @return identyfikator żądania */
    }
}
