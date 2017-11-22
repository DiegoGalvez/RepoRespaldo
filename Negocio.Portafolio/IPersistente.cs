using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio
{
    interface IPersistente
    {
        bool Create();
        bool Read();
        bool Update();
        bool Delete();
    }
}
