namespace TiendaConsola.Datos
{
    public class ClienteRegular : Cliente
    {
        public ClienteRegular() : base("Regular", 0m) { }

        public override decimal CalcDescBase(decimal subtotal)
        {
            return 0m;
        }

        public override decimal CalcDescAdicional(decimal subtotal)
        {
            if (subtotal > 500m)
                return subtotal * 0.05m;
            return 0m;
        }
    }
}
