using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograWeb.Data;
using ProyectoPrograWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPrograWeb
{
    public class Init
    {
        public static bool tryToMigrate(ProyectoPrograWebContext dbcontext, ApplicationDbContext identitycontext)
        {
            try
            {
                identitycontext.Database.Migrate();
                dbcontext.Database.Migrate();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool tryCreateDefaultUsersAndRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string strAdministradorRole = "admin";

            string strAdministradorEmail = "usuario.administrador@sistema.local";
            string strAdministradorPassword = "Pa55w.rd";
            string strAdministradorFirstName = "Usuario";
            string strAdministradorLastName = "Administrador";

            if (!tryCreateRoleIfNotExist(roleManager, strAdministradorRole))
                return false;

            if (!tryCreateUserIfNotExistsAndAddRole(userManager, strAdministradorEmail, strAdministradorPassword, strAdministradorRole, strAdministradorFirstName, strAdministradorLastName))
                return false;

            return true;
        }

        private static bool tryCreateRoleIfNotExist(RoleManager<IdentityRole> roleManager, string strRole)
        {
            try
            {
#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
                IdentityRole? oRole = roleManager.FindByNameAsync(strRole).Result;
#pragma warning restore CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".

                if (oRole == null)
                {
                    oRole = new IdentityRole();
                    oRole.Name = strRole;
                    oRole.Id = Guid.NewGuid().ToString();

                    roleManager.CreateAsync(oRole).Wait();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static bool tryCreateUserIfNotExistsAndAddRole(UserManager<ApplicationUser> userManager, string strEmail, string strPassword, string strRole, string strFirstName, string strLastName)
        {
            try
            {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
                ApplicationUser? oUser = userManager.FindByNameAsync(strEmail).Result;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
                if (oUser == null)
                {
                    oUser = new ApplicationUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = strEmail,
                        Email = strEmail,
                        EmailConfirmed = true,
                        FirstName = strFirstName,
                        LastName = strLastName
                    };

                    userManager.CreateAsync(oUser, strPassword).Wait();
                }

                if (oUser != null)
                    userManager.AddToRoleAsync(oUser, strRole).Wait();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool trySeedDefaultData(ProyectoPrograWebContext dbcontext)
        {
            try
            {
                // SEXES
                // MALE, FEMALE
                CreateSexIfNotExists(dbcontext, 1, "Male");
                CreateSexIfNotExists(dbcontext, 2, "Female");

                // SPECIES
                // DOG, HAMSTER, CAT, RABBIT
                CreateSpecieIfNotExists(dbcontext, 1, "Dog");
                CreateBreedIfNotExists(dbcontext, 1, 1, "Chihuahua");
                CreateBreedIfNotExists(dbcontext, 2, 1, "Dutch Hound");
                CreateBreedIfNotExists(dbcontext, 3, 1, "Cocker Spaniel");

                CreateSpecieIfNotExists(dbcontext, 2, "Hamster");
                CreateBreedIfNotExists(dbcontext, 4, 2, "Golden");
                CreateBreedIfNotExists(dbcontext, 5, 2, "Dwarf");

                CreateSpecieIfNotExists(dbcontext, 3, "Cat");
                CreateBreedIfNotExists(dbcontext, 6, 3, "European");
                CreateBreedIfNotExists(dbcontext, 7, 3, "Egyptian");
                CreateBreedIfNotExists(dbcontext, 8, 3, "Siberian");
                CreateBreedIfNotExists(dbcontext, 8, 3, "Persian");

                CreateSpecieIfNotExists(dbcontext, 4, "Rabbit");
                CreateBreedIfNotExists(dbcontext, 9, 4, "American Fuzzy Lop");
                CreateBreedIfNotExists(dbcontext, 10, 4, "Dutch Biler");
                CreateBreedIfNotExists(dbcontext, 11, 4, "Cashmere Lop");

                // STATUSES
                // AVAILABLE, IN ADOPTION PROCESS
                CreateStatusPetIfNotExists(dbcontext, 1, "Available");
                CreateStatusPetIfNotExists(dbcontext, 2, "In Adoption Process");

                // ENERGY LEVELS
                CreateEnergyLevelIfNotExists(dbcontext, 1, "Reserved");
                CreateEnergyLevelIfNotExists(dbcontext, 2, "Playful");
                CreateEnergyLevelIfNotExists(dbcontext, 3, "Affectionate");
                CreateEnergyLevelIfNotExists(dbcontext, 4, "Independent");
                CreateEnergyLevelIfNotExists(dbcontext, 5, "Intelligent");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static Breed CreateBreedIfNotExists(ProyectoPrograWebContext dbcontext, int Id, int IdSpecie, string descripcion)
        {
            var obj = dbcontext.Breeds.Where(x => x.IdBreed == Id);

            if (!obj.Any())
            {
                Breed o = new Breed()
                {
                    IdSpecieRace = IdSpecie,
                    NameBreed = descripcion
                };

                dbcontext.Breeds.Add(o);
                dbcontext.SaveChanges();

                return o;
            }

            return null;
        }

        private static Specie CreateSpecieIfNotExists(ProyectoPrograWebContext dbcontext, int Id, string descripcion)
        {
            var obj = dbcontext.Species.Where(x => x.IdSpecie == Id);

            if (!obj.Any())
            {
                Specie o = new Specie()
                {
                    NameSpecie = descripcion
                };

                dbcontext.Species.Add(o);
                dbcontext.SaveChanges();

                return o;
            }

            return null;
        }

        private static Sex CreateSexIfNotExists(ProyectoPrograWebContext dbcontext, int Id, string descripcion)
        {
            var obj = dbcontext.Sexes.Where(x => x.IdSex == Id);

            if (!obj.Any())
            {
                Sex o = new Sex()
                {
                    NameSex = descripcion
                };

                dbcontext.Sexes.Add(o);
                dbcontext.SaveChanges();

                return o;
            }

            return null;
        }

        private static StatusPet CreateStatusPetIfNotExists(ProyectoPrograWebContext dbcontext, int Id, string descripcion)
        {
            var obj = dbcontext.StatusPets.Where(x => x.IdStatus == Id);

            if (!obj.Any())
            {
                StatusPet o = new StatusPet()
                {
                    NameStatus = descripcion
                };

                dbcontext.StatusPets.Add(o);
                dbcontext.SaveChanges();

                return o;
            }

            return null;
        }

        private static EnergyLevel CreateEnergyLevelIfNotExists(ProyectoPrograWebContext dbcontext, int Id, string descripcion)
        {
            var obj = dbcontext.EnergyLevels.Where(x => x.LevelId == Id);

            if (!obj.Any())
            {
                EnergyLevel o = new EnergyLevel()
                {
                    LevelName = descripcion
                };

                dbcontext.EnergyLevels.Add(o);
                dbcontext.SaveChanges();

                return o;
            }

            return null;
        }
    }
}
