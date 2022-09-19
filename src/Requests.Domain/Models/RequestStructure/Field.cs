using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Domain.Models.RequestStructure
{
    public class Field
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Label { get; set; }

        public Option[]? Options { get; set; }

        public object? Value { get; set; }

        public bool Required { get; set; }

        public bool Readonly { get; set; }
    }
}
