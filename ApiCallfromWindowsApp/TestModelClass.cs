using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCallfromWindowsApp
{
    public class TestModelClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile formFiles { get; set; }
    }
}
