using System.ComponentModel.DataAnnotations;

namespace ParkyWebAPI.Models.DTOs
{
    public class NationalParkDTOs
    {        
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string State { get; set; }
        public DateTime Created { get; set; }
        //public byte[] Picture { get; set; }
        public DateTime Established { get; set; }
    }
}
