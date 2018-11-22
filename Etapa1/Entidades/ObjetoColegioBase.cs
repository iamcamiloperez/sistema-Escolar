using System;
namespace ColegioCore.Entidades
{
    public abstract class ObjetoColegioBase
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }

        public ObjetoColegioBase()
        {
            UniqueId/* */= Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre} - Id: {UniqueId}";
        }
    }
}