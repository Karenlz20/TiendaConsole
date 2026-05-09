namespace TiendaConsola.Datos
{
    public abstract class Cliente
    {
        private string tipoCliente;
        protected decimal descuentoBase;

        public Cliente(string tipoCliente, decimal descuentoBase)
        {
            this.tipoCliente = tipoCliente;
            this.descuentoBase = descuentoBase;
        }

        public string GetTipoCliente() { return tipoCliente; }
        public decimal GetDescuentoBase() { return descuentoBase; }

        public abstract decimal CalcDescBase(decimal subtotal);
        public abstract decimal CalcDescAdicional(decimal subtotal);

        public decimal CalcTotalDescuento(decimal subtotal)
        {
            return CalcDescBase(subtotal) + CalcDescAdicional(subtotal);
        }
    }
}
