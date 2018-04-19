namespace VaucherSystem.Models.EntityModels
{
    using System;

    public class Picture
    {
        public int Id { get; set; }

        public int VaucherId { get; set; }
        public virtual Vaucher Vaucher { get; set; }
        public byte[] FileData { get; set; }
    }
}
