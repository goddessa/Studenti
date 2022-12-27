using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class IspitniRok
    {

        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Naziv { get; set; } = null!;
        //public int Ocena { get; set; }
        //više na 1 u odnosu na predmete:
        //public Predmet Predmet {get; set;} //ne mapira se ali kreira foreign key
       // public Student Student {get; set;}
        public virtual List<Spoj>? StudentiPredmeti { get; set; } = null!;
        
}

}