using practicheckaya_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practicheckaya_3
{
    public class Helper
    {
        private static ShoesFabricDBEntities _context;

        public static ShoesFabricDBEntities GetContext()
        {
            if (_context == null)
            {
                _context = new ShoesFabricDBEntities();
            }
            return _context;
        }
    }
}
