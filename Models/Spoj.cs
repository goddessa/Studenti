using System.ComponentModel.DataAnnotations;


namespace Models;
    public class Spoj
    {
  
        [Key]
        public int ID { get; set; }

        [Range(5, 10)]
        public int Ocena { get; set; }
         //public virtual IspitniRok IspitniRok { get; set; }

        public Student? Student { get; set; } = null!;

        public Predmet? Predmet { get; set; } = null;
       public List<IspitniRok>? IspitniRok { get; set;}
    }




       /* [Key]
        public int ID { get; set; }
        [Range(5, 10)]
        public int Ocena { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Predmet? Predmet { get; set; }
    
    public List<IspitniRok>? IspitniRok { get; set; } //u kom ispitnom roku je položen predmet
    
*/
        //predstavlja Predmet i Student, njihova veza
        //predmet ima više ispitnih rokova
        //rok ima više predmeta koji se polažu u njemu
 
