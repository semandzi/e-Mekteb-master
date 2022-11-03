using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using e_Mekteb.ViewModel;
using e_Mekteb.ViewModel.Muftija;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Controllers
{
    [Authorize(Roles = "Muftija")]

    public class MuftijaController : Controller
    {
        private UserManager<AplicationUser> userManager;
        private RoleManager<IdentityRole> _roleManager;
        private e_MektebDbContext _context;

        public MuftijaController(RoleManager<IdentityRole> roleManager, UserManager<AplicationUser> userManager, e_MektebDbContext context)
        {
            this.userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;

        }


        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var numberOfStudents = _context.RazrediUcenik.Where(d => d.DatumIspisa == DateTime.MinValue).ToList();
            var users = _context.VjerouciteljUcenik.OrderBy(i => i.UserName)
                         .Select(u => u.UcenikId).ToList();

            //ViewBag.BrojUcenika = numberOfStudents.Count();
            ViewBag.BrojUcenika = users.Count();

            var tempUcenikProfilFlag = new List<StudentProfilFlag>();
            string tempNazivLokacije = "";
            string tempRazred = "";
            DateTime datumUpisa = DateTime.MinValue;


            foreach (var id in users)
            {
                var user = await userManager.FindByIdAsync(id);
                //SkoleUcenika
                var skoleUcenika = _context.SkoleUcenika.Where(s => s.UcenikId == user.Id).ToList();

                //Razred ucenika
                var razrediUcenikaKodOvogVjeroucitelja = _context.RazrediUcenik.Where(u => u.UcenikId == user.Id && u.DatumIspisa == DateTime.MinValue).ToList();
                var razrediUcenikaKodDrugogVjeroucitelja = _context.RazrediUcenik.Where(u => u.UcenikId == user.Id && u.DatumIspisa == DateTime.MinValue).ToList();


                //Skole i razredi

                if (razrediUcenikaKodOvogVjeroucitelja.Any())
                {
                    var skoleRazredi = razrediUcenikaKodOvogVjeroucitelja.Join(skoleUcenika,
                                                       r => r.UcenikId,
                                                       s => s.UcenikId,
                                                       (razredi, skole) => new
                                                       {
                                                           Razred = razredi.Razred,
                                                           Skole = skole.NazivSkole
                                                       });

                    foreach (var razredi in skoleRazredi)
                    {
                        tempRazred = razredi.Razred;
                        tempNazivLokacije = razredi.Skole;

                    }
                }
                else
                {
                    //tempRazred = razredi.Razred;
                    //tempNazivLokacije = razredi.Skole;
                    tempRazred = "Razred nije upisan";
                    tempNazivLokacije = "Škola/Lokacija nije unesena";


                }

                //Godina trenutna i datum upisa
                var result1 = razrediUcenikaKodOvogVjeroucitelja.Join(_context.SkolskeGodine,
                                                r => r.SkolskaGodinaId,
                                                s => s.SkolskaGodinaId,
                                                (datum_Upisa, godina) => new
                                                {
                                                    Datum = datum_Upisa.DatumUpisa,
                                                    Godina = godina.Godina
                                                });
                foreach (var godina in result1)
                {
                    datumUpisa = godina.Datum;
                    ViewBag.Godina = godina.Godina.ToString();
                }

                //Naziv medzlisa
                var vjeroucitelj_Id = _context.VjerouciteljUcenik
                    .Select(v => v.VjerouciteljId).FirstOrDefault();
                var user_Vjeroucitelj = await userManager.FindByIdAsync(vjeroucitelj_Id);
                var nazivMjestaUlogiranogVjeroucitelja = user_Vjeroucitelj.NazivMjesta.ToString();

                ViewBag.Medzlis = nazivMjestaUlogiranogVjeroucitelja;
                ViewBag.Naziv = nazivMjestaUlogiranogVjeroucitelja;



                //if (razrediUcenikaKodOvogVjeroucitelja.Any())
                //{
                //    var result2 = razrediUcenikaKodOvogVjeroucitelja.Join(_context.Medzlisi,
                //                            r => r.MedzlisId,
                //                            m => m.MedzlisId,

                //                            (naziv, medzlis) => new
                //                            {
                //                                MedzlisId = naziv.MedzlisId,
                //                                Naziv = medzlis.Naziv

                //                            });
                //    foreach (var medzlis in result2)
                //    {
                //        ViewBag.Medzlis = medzlis.Naziv;
                //        ViewBag.Naziv = medzlis.Naziv;
                //    }
                //}
                //else {
                //    var result2 = razrediUcenikaKodDrugogVjeroucitelja.Join(_context.Medzlisi,
                //                    r => r.MedzlisId,
                //                    m => m.MedzlisId,

                //                    (naziv, medzlis) => new
                //                    {
                //                        MedzlisId = naziv.MedzlisId,
                //                        Naziv = medzlis.Naziv

                //                    });
                //    foreach (var medzlis in result2)
                //    {
                //        ViewBag.Medzlis = medzlis.Naziv;
                //        ViewBag.Naziv = medzlis.Naziv;
                //    }

                //}



                //Provjera dali je popunjen profil dokraja, inicijalizira flag na 0 ili 1
                if (user.Ulica == null || user.PostanskiBroj == null || user.DatumRodenja == DateTime.MinValue || user.ImeiPrezime == null || user.BrojMobitela == null ||
                    user.ImeiPrezime == null || user.Email == null || user.UserName == null)
                {
                    int flag = 0;
                    var tempmodel = new StudentProfilFlag
                    {
                        AplicationUser = user,
                        Flag = flag,
                        Datum = datumUpisa,
                        Razred = tempRazred,
                        LokacijaNastave = tempNazivLokacije

                    };

                    if (tempUcenikProfilFlag.Contains(tempmodel))
                    {
                        continue;
                    }
                    else { tempUcenikProfilFlag.Add(tempmodel); }

                }
                else
                {
                    var flag = 1;
                    var tempmodel = new StudentProfilFlag
                    {
                        AplicationUser = user,
                        Flag = flag,
                        Datum = datumUpisa,
                        Razred = tempRazred,
                        LokacijaNastave = tempNazivLokacije

                    };
                    if (tempUcenikProfilFlag.Contains(tempmodel))
                    {
                        continue;
                    }
                    else { tempUcenikProfilFlag.Add(tempmodel); }

                }


            }

            tempUcenikProfilFlag.OrderBy(x => x.AplicationUser.ImeiPrezime).ToList();
            var vjerouciteljListaUcenika = new TeacherListOfStudents
            {
                Profili = tempUcenikProfilFlag
            };
            return View(vjerouciteljListaUcenika);
        }



        [HttpGet]
        public IActionResult GetReligiousCommunities()
        {
            var religiousCommunities = _context.Medzlisi.ToList();
            var studentClasses = _context.RazrediUcenik.Where(u => u.DatumIspisa == DateTime.MinValue).ToList();
            var tempList = new List<MuftijaMedzlisViewModel>();

            int count = 0;
            var teacher = religiousCommunities.Join(studentClasses,
                                    community => community.MedzlisId,
                                    student => student.MedzlisId,
                                    (community, student) => new
                                    {
                                        Community = community.Naziv,
                                        Student = student.UcenikId


                                    }).OrderBy(c => c.Community).ToList();

            religiousCommunities.ForEach(m =>
            {
                count = 0;

                teacher.ForEach(medzlis =>
                {
                    if (medzlis.Community == m.Naziv)
                    {
                        count += 1;
                    }

                });

                var muftijaMedzlis = new MuftijaMedzlisViewModel
                {
                    BrojUcenika = count,
                    NazivMedzlisa = m.Naziv,
                    MedzlisId = m.MedzlisId

                };

                tempList.Add(muftijaMedzlis);

            });

            return View(tempList);
        }

        //Get all teachers from selected community
        public IActionResult GetTeachersFromCommunity(int medzlisId)
        {
            var nameOfMedzlis = _context.Medzlisi.Where(m => m.MedzlisId == medzlisId).Select(n => n.Naziv).SingleOrDefault().ToString();
            var nameOfTownOfMedzlis = nameOfMedzlis.Substring(4);
            var teachers = userManager.GetUsersInRoleAsync("Vjeroucitelj").Result.Where(n => n.NazivMjesta == nameOfTownOfMedzlis).ToList();
            var tempList = new List<MuftijaVjerouciteljiViewModel>();


            teachers.ForEach(t =>
            {
                var studentsCount = _context.RazrediUcenik.Where(m => m.MedzlisId == medzlisId && m.VjerouciteljId == t.AplicationUserId && m.DatumIspisa == DateTime.MinValue).ToList().Count();
                var muftijaVjerouciteljiViewModel = new MuftijaVjerouciteljiViewModel
                {
                    BrojUcenika = studentsCount,
                    ImeiPrezime = t.ImeiPrezime,
                    VjerouciteljId = t.AplicationUserId

                };

                tempList.Add(muftijaVjerouciteljiViewModel);
            });
            return View(tempList);
        }

        public IActionResult GetAllSchoolsFromTeacher(string vjerouciteljId) {
            var teacherSchools = _context.Skole.Where(v => v.VjerouciteljId == vjerouciteljId).ToList();
            var tempList = new List<MuftijaSkoleViewModel>();
            teacherSchools.ForEach(s =>
            {
                var studentsThatAreEnrolledInSchool = _context.RazrediUcenik.Where(m => m.VjerouciteljId == vjerouciteljId && m.SkolaId== s.SkolaId && m.DatumIspisa == DateTime.MinValue).ToList().Count();
                var muftijaSkoleViewModel = new MuftijaSkoleViewModel
                {
                    NazivSkole = s.NazivSkole,
                    UpisaniBrojUcenika = studentsThatAreEnrolledInSchool,
                    SkolaId=s.SkolaId
                };
                tempList.Add(muftijaSkoleViewModel);
            });
            return View(tempList);
        }

        //public IActionResult GetStudentsFromSchool(int skolaId) {
        //    var studentsFromShools= _context.RazrediUcenik.Where(m => m.SkolaId == m.SkolaId && m.DatumIspisa == DateTime.MinValue).SelectMany(r=>r.UcenikId,,).ToList().Count();

        //    return View();
        //}

    }

}


















