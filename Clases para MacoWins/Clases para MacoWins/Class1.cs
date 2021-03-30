using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_para_MacoWins
{
    public class Local
    {
        public List<Venta> ventas { get; set; } //Creo una lista de ventas para poder tener presente todas las ventas que se hicieron y porque es más fácil de usar en caso de querer mostrar las ventas que se produjeron 

        public Local()
        {
            ventas = new List<Venta>();
        }

        public float calculo_GananciaNeta_diaria(DateTime fecha)
        {
            float ganancia = 0;

            foreach(Venta v in ventas)
            {
                if(v.fecha.Date == fecha.Date) //Verifico si la venta fue efectuada el día que se está buscando
                {
                    ganancia += v.ganancia; //Si la venta fue realizada ese día acumulo la ganancia de esta venta en ganancia
                }
            }

            return ganancia; //Devuelvo la ganancia total del día
        }

        public float calculo_GananciaBruta_diaria(DateTime fecha) //Sinceramente puse un método para ganancia bruta y uno para ganancia neta porque al leer "saber las ganancias del día" no estaba del todo seguro a cual de las dos hacía referencia
        {
            float ganancia = 0;

            foreach (Venta v in ventas)
            {
                if (v.fecha.Date == fecha.Date) //Verifico si la venta fue efectuada el día que se está buscando
                {
                    ganancia += v.pago; //Si la venta fue realizada ese día acumulo la ganancia bruta de esa venta
                }
            }

            return ganancia; //Devuelvo la ganancia total del día
        }
    }
    
    public class Prenda
    {
        public float precio_nuevo { get; set; }
        public string estado { get; set; }
        public float coste { get; set; }
    }

    public class Venta
    {
        public Prenda prenda { get; set; }
        public int cantidad { get; set; }
        public DateTime fecha { get; set; }
        public float ganancia { get; set; } 
        public float pago { get; set; }

        public Venta(Prenda p, int cant, DateTime f) //Constructor para cuando pagan en efectivo
        {
            prenda = p;
            cantidad = cant;
            fecha = f;

            if (prenda.estado == "nuevo")
            {
                pago = prenda.precio_nuevo * cantidad;
            }
            else if (prenda.estado == "promocion")
            {
                pago = prenda.precio_nuevo * (float)0.9 * cantidad; //Si la prenda está en promocion le puse que cuesta un 10% menos
            }
            else //(prenda.estado == "liquidacion")
            {
                pago = (prenda.precio_nuevo / 2) * cantidad;
            }

            ganancia = pago - (prenda.coste * cantidad);
        }

        public Venta(Prenda p, int cant, DateTime f, int cuotas) //Constructor para cuando pagan con tarjeta
        {
            prenda = p;
            cantidad = cant;
            fecha = f;
                        
            //Para calcular el valor del pago en el caso de la targeta hago lo mismo pero reemplazando el "prenda.precio_nuevo" de la venta en efectivo con la fórmula para calcular el precio agregándole el valor del recargo

            if (prenda.estado == "nuevo")
            {
                pago = (prenda.precio_nuevo + cuotas * (float)0.02 * prenda.precio_nuevo + prenda.precio_nuevo * (float)0.01) * cantidad; //El coeficiente fijo lo pongo como el 2%
            }
            else if (prenda.estado == "promocion")
            {
                pago = (prenda.precio_nuevo + cuotas * (float)0.02 * prenda.precio_nuevo + prenda.precio_nuevo * (float)0.01) * (float)0.9 * cantidad; //Si la prenda está en promocion le puse que cuesta un 10% menos
            }
            else //(prenda.estado == "liquidacion")
            {
                pago = ((prenda.precio_nuevo + cuotas * (float)0.02 * prenda.precio_nuevo + prenda.precio_nuevo * (float)0.01) / 2) * cantidad;
            }

            ganancia = pago - (prenda.coste * cantidad); 
        }       
    }

    public class Saco : Prenda
    {
    }

    public class Pantalon : Prenda
    {
    }

    public class Camisa : Prenda
    {
    }
}
