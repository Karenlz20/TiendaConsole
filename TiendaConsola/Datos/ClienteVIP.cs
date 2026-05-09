namespace TiendaConsola.Datos
{
    public class ClienteVIP : Cliente
    {
        public ClienteVIP() : base("VIP", 0.10m) { }

        public override decimal CalcDescBase(decimal subtotal)
        {
            return subtotal * 0.10m;
        }

        public override decimal CalcDescAdicional(decimal subtotal)
        {
            if (subtotal > 500m)
                return subtotal * 0.05m;
            return 0m;
        }
    }
}
