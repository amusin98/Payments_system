using Payments_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system
{
    public class StartData
    {
        public static void Initialize(PaymentsContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        IsAdmin = false,
                        Name = "Nikolay",
                        Surname = "Amusin",
                        Email = "amusin98@gmail.com",
                        Password = "AsDiS9820"
                    },
                    new User
                    {
                        IsAdmin = true,
                        Name = "Ivan",
                        Surname = "Ivanov",
                        Email = "ivanov@gmail.com",
                        Password = "11111111"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Kirill",
                        Surname = "Stepanov",
                        Email = "kirill@gmail.com",
                        Password = "44444444"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Andrei",
                        Surname = "Spiridonov",
                        Email = "andre@mail.ru",
                        Password = "12345678"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Ivan",
                        Surname = "Litvinov",
                        Email = "ivan@gmail.com",
                        Password = "45236235"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Alexei",
                        Surname = "Platonov",
                        Email = "platoha@gmail.com",
                        Password = "74125896"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Nikolay",
                        Surname = "Andreev",
                        Email = "nick@gmail.com",
                        Password = "78965231"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Konstantin",
                        Surname = "Kom",
                        Email = "koctya@gmail.com",
                        Password = "77777777"
                    },
                    new User
                    {
                        IsAdmin = true,
                        Name = "Konstantin",
                        Surname = "Kurochka",
                        Email = "kurochka@gstu.by",
                        Password = "ks111111"
                    },
                    new User
                    {
                        IsAdmin = true,
                        Name = "Igor",
                        Surname = "Stefanovskiy",
                        Email = "igor@gmail.com",
                        Password = "igorigor"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Kirill",
                        Surname = "Sidorov",
                        Email = "sid@mail.ru",
                        Password = "sidor123"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Kseniya",
                        Surname = "Zlotnikova",
                        Email = "zltnkv@gmail.com",
                        Password = "55555555"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Ilya",
                        Surname = "Puninskiy",
                        Email = "punya@gmail.com",
                        Password = "punya777"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Alexandr",
                        Surname = "Anosov",
                        Email = "anos@mail.ru",
                        Password = "85225523"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Nikolay",
                        Surname = "Ropot",
                        Email = "ropot@gmail.com",
                        Password = "ropot123"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Ivan",
                        Surname = "Kosov",
                        Email = "kokos@gmail.com",
                        Password = "kokos1"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Oleg",
                        Surname = "Ivanov",
                        Email = "oleg@gstu.by",
                        Password = "77778888"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Alexey",
                        Surname = "Kachan",
                        Email = "kachan@gstu.by",
                        Password = "lexa8888"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Sergei",
                        Surname = "Gusakov",
                        Email = "gus@mail.ru",
                        Password = "gusserega"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Alexandr",
                        Surname = "Obrezkov",
                        Email = "alex@gmail.com",
                        Password = "alex777"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Dmitriy",
                        Surname = "Kozintsov",
                        Email = "kozinak@gmail.com",
                        Password = "kozinak"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Oleg",
                        Surname = "Asenchik",
                        Email = "oleg.asenchik@gmail.com",
                        Password = "oleg7777"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Pavel",
                        Surname = "Shur",
                        Email = "shur@mail.ru",
                        Password = "7523shur"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Viktoriya",
                        Surname = "Belous",
                        Email = "vika@gmail.com",
                        Password = "vikavika"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Vladislav",
                        Surname = "Volotkovich",
                        Email = "volot@gmail.com",
                        Password = "44455566"
                    },
                    new User
                    {
                        IsAdmin = true,
                        Name = "Denis",
                        Surname = "Trubenok",
                        Email = "denis@gmail.com",
                        Password = "den4ikturbo"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Alesya",
                        Surname = "Lizogub",
                        Email = "lizogub@mail.ru",
                        Password = "alesya78"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Vitaliy",
                        Surname = "Belodedov",
                        Email = "beloded@gmail.com",
                        Password = "belodedov"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Diana",
                        Surname = "Korniluk",
                        Email = "dianka@gmail.com",
                        Password = "korniluk72"
                    },
                    new User
                    {
                        IsAdmin = false,
                        Name = "Yana",
                        Surname = "Kislyak",
                        Email = "yana@gmail.com",
                        Password = "yanatopor"
                    });
                context.SaveChanges();
            }
            double[] balances = new Double[1300];
            for (int i = 0; i < balances.Length; i++)
            {
                balances[i] = new Random().Next(20, 250);
            }
            if (!context.Cards.Any())
            {
                for (int i = 1, k = 0; i <= 30; i++)
                {
                    for (int j = 0; j < 35; j++, k++)
                    {
                        context.Cards.Add(new Card { UserId = i, Balance = balances[k] });
                    }
                }
                context.SaveChanges();

            }
            if (!context.Accounts.Any())
            {
                for (int i = 1, j = 0; i <= 1000; i++, j++)
                {
                    context.Accounts.Add(new Account { CardId = i, IsBlocked = false, Balance = balances[j] });
                }
                context.SaveChanges();
            }
            if (!context.Goals.Any())
            {
                context.Goals.AddRange(
                    new Goal
                    {
                        GoalName = "Learning"
                    },
                    new Goal
                    {
                        GoalName = "Gym"
                    },
                    new Goal
                    {
                        GoalName = "BRSM"
                    },
                    new Goal
                    {
                        GoalName = "TV"
                    },
                    new Goal
                    {
                        GoalName = "Phone"
                    },
                    new Goal
                    {
                        GoalName = "Bank"
                    },
                    new Goal
                    {
                        GoalName = "Shop"
                    },
                    new Goal
                    {
                        GoalName = "Oil"
                    },
                    new Goal
                    {
                        GoalName = "Tax"
                    },
                    new Goal
                    {
                        GoalName = "Credit"
                    },
                    new Goal
                    {
                        GoalName = "Penalty"
                    },
                    new Goal
                    {
                        GoalName = "Cinema"
                    },
                    new Goal
                    {
                        GoalName = "Teacher"
                    },
                    new Goal
                    {
                        GoalName = "Software"
                    },
                    new Goal
                    {
                        GoalName = "Retake"
                    },
                    new Goal
                    {
                        GoalName = "Communal"
                    },
                    new Goal
                    {
                        GoalName = "Home"
                    },
                    new Goal
                    {
                        GoalName = "Pool"
                    },
                    new Goal
                    {
                        GoalName = "Referee"
                    },
                    new Goal
                    {
                        GoalName = "Project"
                    },
                    new Goal
                    {
                        GoalName = "Water"
                    },
                    new Goal
                    {
                        GoalName = "Hostel"
                    },
                    new Goal
                    {
                        GoalName = "Dormitory"
                    },
                    new Goal
                    {
                        GoalName = "Mortage"
                    },
                    new Goal
                    {
                        GoalName = "Game"
                    },
                    new Goal
                    {
                        GoalName = "Microsoft Office"
                    },
                    new Goal
                    {
                        GoalName = "Matlab"
                    },
                    new Goal
                    {
                        GoalName = "Notebook"
                    },
                    new Goal
                    {
                        GoalName = "Garage"
                    },
                    new Goal
                    {
                        GoalName = "Parking"
                    });

                context.SaveChanges();
            }
            if (!context.Payments.Any())
            {
                    for (int i = 1; i <= 1000; i++)
                    {
                        context.Payments.Add(new Payment { AccountId = new Random().Next(1,1000), GoalId = new Random().Next(1,30), Date = "01/01/201" + new Random().Next(1,9), Amount = new Random().Next(10,150)});
                    }
                context.SaveChanges();
            }
            
        }
    }
}
