using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Vežbanje.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
        public StudentController(FakultetContext context) 
        {
            this.Context = context;
   
        }
            public FakultetContext Context { get; set; }

  

    [Route("Preuzmi")] //da vratimo sve što nam je potrebno, koji rok i koji predmet
    [HttpGet]
    public async Task<ActionResult> Preuzmi()
    {
        //eksplicit loading
        var studenti = Context.Studenti
        .Include(p => p.StudentPredmet)
       .ThenInclude(p => p.IspitniRok)
        .Include(p => p.StudentPredmet)
        .ThenInclude(p => p.Predmet);
        var student = studenti.Where(p => p.Indeks == 12345).FirstOrDefault();
        await Context.Entry(student).Collection(p => p.StudentPredmet).LoadAsync();
        foreach (var s in student.StudentPredmet)
        {
           await Context.Entry(s).Reference(q => q.IspitniRok).LoadAsync();
            await Context.Entry(s).Reference(q => q.Predmet).LoadAsync();
        }
        
      // var student = await studenti.ToListAsync();


    return Ok(Context.Studenti);
    }


        [Route("DodatiStudenta")]
        [HttpPost]
        public async Task<ActionResult> DodajStudenta([FromBody] Student student)
        {
            if (student.Indeks < 10000 || student.Indeks > 20000)
            {
                return BadRequest("Pogrešan Indeks!");
            }

            if (string.IsNullOrWhiteSpace(student.Ime) || student.Ime.Length > 50)
            {
                return BadRequest("Pogrešno ime!");
            }

            if (string.IsNullOrWhiteSpace(student.Prezime) || student.Prezime.Length > 50)
            {
                return BadRequest("Pogrešno prezime!");
            }

            try
            {
                Context.Studenti.Add(student);
                await Context.SaveChangesAsync();
                return Ok($"Student je dodat! ID je: {student.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
  [Route("PromenitiStudenta")]
  [HttpPut]
  public async Task<ActionResult> Promeni(int indeks, string ime, string prezime) //promenimo 1 ili 2 propertija ili celog studenta
  {
    if(indeks <10000 || indeks > 20000)
    {
        return BadRequest("Pogrešan indeks");
    }
    try
    {
         var student = Context.Studenti.Where( p => p.Indeks == indeks).FirstOrDefault(); 
         if (student != null)
         {
            student.Ime = ime;
            student.Prezime = prezime;
            await Context.SaveChangesAsync();
            return Ok($" Uspešna promena! ID: {student.ID}");

         }
         else
         {
            return BadRequest("Student nije pronađen!");
         }
            return Ok();
    }
    catch (Exception e)
    {

    }
    //prvi koji zadovoljava uslov ili vraća null
    return Ok();
  }

  [Route("PromenaFromBody")]
  [HttpPut]
  public async Task<ActionResult> PromeniBody([FromBody] Student student)
  {
    if(student.ID <= 0)
    {
        return BadRequest ("Pogrešan id");
        //ostale provere za ime, indeks, prezime kao iznad

    }
    try
    {
        var studentZaPromenu = await Context.Studenti.FindAsync();
       /* studentZaPromenu.Indeks = student.Indeks;
        studentZaPromenu.Ime = student.Ime;
        studentZaPromenu.Prezime = student.Prezime;
        */
        Context.Studenti.Update(student);
        await Context.SaveChangesAsync();
        return Ok($"Student sa IDem: {student.ID} je uspešno promenjen!");
    }
    catch(Exception e)
    {
        return BadRequest(e.Message); 

    }
  }

  [Route("IzbrisatiStudenta/{id}")]
  [HttpDelete]
  public async Task<ActionResult> Izbrisi(int id)
  {
    if(id <= 0)
    {
        return BadRequest("Pogrešan id");
    }
    try
    {
        var student = await Context.Studenti.FindAsync(id);
        Context.Studenti.Remove(student);
        await Context.SaveChangesAsync();
        return Ok($"Uspešno izbrisan student sa IDem: {student.Indeks}");
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
  }



}
