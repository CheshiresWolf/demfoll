using Utils;

namespace Generator {
    public class Collection {
        public string[] names;
        public int[] probs;

        public Collection(string[] default_names, int[] default_probs) {
            names = default_names;
            probs = default_probs;
        }
    }

    public class PersonsGenerator {
        const string GENDER_MAN = "муж";
        const string GENDER_WOMAN = "жен";

        RandomUtils utils = new RandomUtils();
        Biography biography = new Biography();
        Perks perks = new Perks();

        public Person[] run(int number) {
            Person[] persons = new Person[number];

            Collection manBiography = readBiographyCollection(GENDER_MAN);
            Collection womanBiography = readBiographyCollection(GENDER_WOMAN);

            for (int i = 0; i < number; i++) {
                persons[i] = new Person();

                persons[i].id = i;

                persons[i].nation = utils.getRandomFromCollection(readNationsCollection());
                persons[i].gender = utils.getRandomFromArrays(
                    new string[2] { GENDER_MAN, GENDER_WOMAN },
                    new int[2] { 90, 10 }
                );

                persons[i].name = utils.getRandomFromCollection(
                    readNamesCollection(persons[i].gender, persons[i].nation)
                );
                persons[i].surname = utils.getRandomFromCollection(
                    readSurnamesCollection(persons[i].nation)
                );

                persons[i].age = utils.getRandomBetween(20, 31);

                biography.apply(persons[i], utils.getRandomFromCollection(persons[i].gender == GENDER_MAN ? manBiography : womanBiography));
                biography.apply(persons[i], utils.getRandomFromCollection(persons[i].gender == GENDER_MAN ? manBiography : womanBiography));

                persons[i].nature = utils.getRandomFromArrays(
                    new string[3] { "Good", "Neutral", "Bad" },
                    new int[3] { 25, 50, 25 }
                );

                checkAbilities(persons[i]);
                checkAge(persons[i]);

                persons[i].money = utils.getRandomBetween(0, 31);
            }

            return persons;
        }

        // ======= <Read Collections> =======

        private Collection readNationsCollection() {
            return new Collection(
                new string[2] { "Споглед", "Баскар" },
                new int[2] { 70, 30 }
            );
        }

        private Collection readNamesCollection(string gender, string nation) {
            string[] names = new string[0];

            switch (nation) {
                case "Споглед":
                    if (gender == GENDER_MAN) {
                        names = new string[90] { "Ctibor", "Nicolae", "Dimitrie", "Valentin", "Remus", "Petar", "Blažej", "Gaweł", "Albin", "Jacek", "Tomek", "Dragomir", "Bartosz", "Constantin", "Răzvan", "Bronisław", "Henryk", "Artur", "Filip", "Gracjan", "Mścisław", "Kazimierz", "Władysław", "Oleg", "Corneliu", "Věnceslav", "Franciszek", "Teobald", "Jan", "Andrzej", "Mateusz", "Bartłomiej", "Justyn", "Mirosław", "Szymon", "Arnold", "Bolesław", "Velkan", "Adam", "Witołd", "Janusz", "Stanisław", "Damian", "Lew", "Ernest", "Sebastian", "Dariusz", "Józef", "Krzysztof", "Grzegorz", "Marek", "Wolodymyr", "Roman", "Emil", "Codrin", "Igor", "Czesław", "Karlo", "Michał", "Łukasz", "Dawid", "Waldemar", "Marián", "Luděk", "Bartek", "Maxmilián", "Gustaw", "Miłosz", "Zikmund", "Aron", "Jarosław", "Eugen", "Antoni", "Augustyn", "Kasper", "Tadeusz", "Zdzisław", "Jaromir", "Iosif", "Serafin", "Leopold", "Aleksy", "Eduard", "Bogumił", "Costel", "Cristian", "Paul", "Walerian", "Darek", "Wacław" };
                    } else {
                        names = new string[90] { "Anastasia", "Irma", "Sorina", "Milena", "Ruta", "Janina", "Tereza", "Daniela", "Brygida", "Běla", "Karolina", "Wiesława", "Lucia", "Kathryn", "Urszula", "Ramona", "Roxana", "Ileana", "Irena", "Daria", "Flavia", "Crina", "Cătălina", "Magda", "Salomea", "Adela", "Berta", "Viorica", "Ewelina", "Alicja", "Nikoleta", "Janita", "Maria", "Simona", "Benedykta", "Zora", "Marika", "Melánie", "Valentina", "Anna", "Zlata", "Klara", "Berenika", "Miloslava", "Jana", "Aneta", "Vasilica", "Iva", "Marianna", "Olga", "Gloria", "Czesława", "Miriam", "Domiana", "Vera", "Eunika", "Blanka", "Valeria", "Iwona", "Stanisława", "Roksana", "Czesława", "Edita", "Mirela", "Agnieszka", "Justyna", "Matylda", "Jaroslava", "Marta", "Cristina", "Jowita", "Estera", "Pelagia", "Valentina", "Ada", "Constanta", "Julie", "Jadwiga", "Augustyna", "Lavinia", "Eleonora", "Olivia", "Nora", "Drahomíra", "Denisa", "Vlasta", "Vasilica", "Sofia", "Ștefania", "Ewelina" };
                    }

                    break;
                case "Баскар":
                    if (gender == GENDER_MAN) {
                        names = new string[90] { "Elwood", "Tony", "Gabriel", "Ryan", "Marshall", "Nowell", "Frederick", "Abraham", "Dexter", "Wilber", "Nathan", "Thadeus", "Willard", "Owen", "Kevin", "Percival", "Amos", "Vernon", "Geoffrey", "Steven", "Patrick", "Reginald", "Daniel", "George", "Mortimer", "Bryan", "Morton", "Graham", "Jasper", "Douglas", "Edward", "Vincent", "Malcolm", "Martin", "Chester", "Eugene", "Mitchell", "Dorian", "Richard", "Henry", "James", "Oscar", "Ethan", "Joshua", "Sebastian", "Kristofer", "Carl", "Adam", "Thomas", "William", "Gareth", "Harold", "Herbert", "Julian", "Jacob", "Lewis", "Victor", "Arthur", "Edmund", "Bertram", "Roland", "Gerard", "Desmond", "Bruce", "Tobias", "Gordon", "John", "Ronald", "Alistair", "Walter", "Everett", "Neil", "Simon", "Nevil", "Floyd", "Albert", "Alfred", "Alan", "Stuart", "Ernest", "Travis", "Anselm", "Ian", "Hervey", "Adrian", "Rowland", "Norman", "Osric", "Hudson", "Clyde" };
                    } else {
                        names = new string[100] { "Marcie", "Bridget", "Amber", "Valerie", "Emma", "Sandra", "Jessica", "Yvonne", "Alberta", "Chloe", "Hilary", "Aurora", "Beverly", "Agnes", "Denise", "Blanche", "Amelia", "Cathleen", "Hannah", "Gina", "Lindsay", "Deborah", "Catherine", "Emilia", "Veronica", "Teresa", "Ashley", "Lillian", "Eloise", "Hazel", "Louise", "Gwyneth", "Millicent", "Virginia", "Helena", "Abigail", "Caroline", "Susan", "Frederica", "Prudence", "Carol", "Myrtle", "Delia", "Alice", "Maria", "Elizabeth", "Gwendolyn", "Jennifer", "Adele", "Hester", "Glenda", "Vivian", "Henrietta", "Lenore", "Victoria", "Joanna", "Rosemary", "Rowena", "Mina", "Wilhelmina", "Magdalen", "Genevieve", "Odette", "Margery", "Delores", "Anna", "Anabel", "Elinor", "Grace", "Nora", "June", "Gwen", "Judith", "Cordelia", "Octavia", "Vanessa", "Daphne", "Yvette", "Fiona", "Charity", "Sylvia", "Mercedes", "Lauren", "Dorothy", "Judith", "Bronwyn", "Brenda", "Amanda", "Ruth", "Eunice", "Shannon", "Carolyn", "Edith", "Rose", "Iris", "Lorelei", "Charlotte", "Paula", "Doris", "Talitha" };
                    }

                    break;
            }

            return new Collection(names, new int[0]);
        }

        private Collection readSurnamesCollection(string nation) {
            string[] names = new string[0];

            switch (nation) {
                case "Споглед":
                    names = new string[90] { "Szczepański", "Flatau", "Jóźwiak", "Miśtal", "Bartosiewicz", "Niemirowski", "Godlewski", "Ptakh", "Hurwicz", "Stefański", "Ozdowski", "Brzechwa", "Juszkiewicz", "Konarski", "Raczkiewicz", "Rutkiewicz", "Baranowski", "Piaseczny", "Urbański", "Adamski", "Bojarski", "Zwoliński", "Anczok", "Grzywacz", "Ossendowski", "Rudziński", "Cieśla", "Pierzchalski", "Leszczyński", "Dobre", "Cyrankiewicz", "Brzeziński", "Jachowski", "Czerwonka", "Chłapowski", "Stachiewicz", "Madejski", "Stanczak", "Sawicki", "Bramski", "Gryphar", "Araszkiewicz", "Piorkowski", "Lewandowski", "Górski", "Naganowski", "Rawicz", "Wilczyński", "Novak", "Wilczek", "Bogusz", "Sikorsky", "Frankowski", "Dawidowski", "Sobolewski", "Podgórski", "Nowitzki", "Wronski", "Phomij", "Kawalec", "Korczak", "Palaszczuk", "Wolski", "Żurawski", "Krzyżewski", "Czaplic", "Giertych", "Bieszczar", "Baginski", "Peszek", "Chorążycki", "Trzebinski", "Bardesh", "Bielawski", "Łebędź", "Kamiński", "Jankowski", "Romanowski", "Czyżewski", "Kowalski", "Bobrowski", "Kaczor", "Morawski", "Kasprzak", "Łuczak", "Krauze", "Sienkiewicz", "Strzałkowski", "Twardowski", "Sarna" };

                    break;
                case "Баскар":
                    names = new string[90] { "Ainsworth", "Spearing", "Smoker", "Stafford", "Jekyll", "Trent", "Stroud", "Shine", "Cotterill", "Garland", "Hamilthon", "Milford", "Frost", "Baines", "Rayne", "Caldwell", "Arrington", "Fairchild", "Shelby", "Hathaway", "Shepard", "Winston", "Wilson", "Pettigrew", "Owston", "Barlow", "Seymour", "Basko", "Alvey", "Archer", "Spencer", "Lock", "Bates", "Clark", "Norwood", "Langdon", "Westley", "Clifford", "Palmer", "Gray", "Parker", "Graves", "Blackwood", "Huxley", "Summers", "Ryder", "Fletcher", "Chandler", "Walker", "Seaman", "Kingsley", "Barton", "Hopkins", "Perkins", "Lawson", "Haight", "Newman", "Young", "Thorn", "Glover", "Clayton", "Ayers", "Wayne", "Ward", "Seward", "Quincey", "Pond", "Johns", "Travers", "White", "Marlow", "Piper", "Carman", "Ridley", "Harlow", "Crawford", "Deadman", "Forester", "Hightower", "Drowner", "Eldridge", "Maynard", "Ross", "Stone", "Truman", "Sandford", "Tolbert", "Warren", "Payne", "Shoreman" };

                    break;
            }

            return new Collection(names, new int[0]);
        }

        private Collection readBiographyCollection(string gender) {
            if (gender == GENDER_MAN) {
                // Солдат +5 RNGC, +3 ATHL & MLC  (15)
                // Жебрак +5 STLTH+INVST (5)  (Л) (Фонд -15)
                // Моряк +5 ATHL & HLTH (++) (15)
                // Колоніст +5 CRFT&WLP (10)
                // Студент +5 INT, +3 INVST & CHAR (5)
                // Аферист +5 CHAR, +3 INT & CONSP (10) (Л)
                // Злочинець +3 MLC & RNGC & STLTH & ATHL (15) (Л)
                // Робочий +5 CRAFT & ATHL (10)
                // Міщанин +5 INT & CMRC (10)
                // Свідок Тінь +5 INVST & CONSP (5)
                // Свідок Окульт +5 MYST & WLP (5)
                // Аристократ +5 CHAR&WLP (5)
                // Богема + 5 INT & CHAR (10)
                // Лікар +5 INT + Перк Лікаря (1)

                return new Collection(
                    new string[14] { "Солдат", "Жебрак", "Моряк", "Колоніст", "Студент", "Аферист", "Злочинець", "Робочий", "Міщанин", "Свідок Тінь", "Свідок Окульт", "Аристократ", "Богема", "Лікар" },
                    new int[14] { 15, 5, 15, 10, 5, 10, 15, 10, 10, 5, 5, 5, 10, 1 }
                );
            } else {
                // Аферист + 5 CHAR, +3 INT & CONSP (15) (Л)
                // Колоніст +5 CRFT&WLP (10)
                // Жебрак +5 STLTH+INVST (5) (Л) (Фонд -15)
                // Студент +5 INT, +3 INVST & CHAR (5)
                // Злочинець +3 MLC & RNGC & STLTH & ATHL (15) (Л)
                // Куртизанка +5 CHAR&CONSP (15) 
                // Міщанка +5 INT & CMRC (10)
                // Свідок Тінь +5 INVST & CONSP (5)
                // Свідок Окульт +5 MYST & WLP (5)
                // Аристократ +5 CHAR & WLP (5) 
                // Робоча +5 CRAFT & ATHL (10)
                // Богема + 5 INT & CHAR (10)
                // Лікар +5 INT + Перк Лікаря (1)

                return new Collection(
                    new string[13] { "Аферист", "Колоніст", "Жебрак", "Студент", "Злочинець", "Куртизанка", "Міщанка", "Свідок Тінь", "Свідок Окульт", "Аристократ", "Робоча", "Богема", "Лікар" },
                    new int[13] { 15, 10, 5, 5, 15, 15, 10, 5, 5, 5, 10, 10, 1 }
                );
            }
        }

        // ======= <Modifiers> =======

        private void checkAbilities(Person person) {
            // Здібності(5 %) - “Геній” +10 або “Невдаха” -10 стати
            string abilitiesModifier = utils.getRandomFromArrays(
                new string[3] { "Геній", "None", "Невдаха" },
                new int[3] { 5, 90, 5 }
            );

            if (abilitiesModifier != "None") perks.apply(person, abilitiesModifier);
        }

        private void checkAge(Person person) {
            // Вік(5 %) - “Юність” 15 років або “Старість” +25 років
            string ageModifier = utils.getRandomFromArrays(
                new string[3] { "Юність", "None", "Старість" },
                new int[3] { 5, 90, 5 }
            );

            if (ageModifier != "None") perks.apply(person, ageModifier);
        }
    }

    public class TeamGenerator {
        public Team[] run(int number) {
            Team[] teams = new Team[number];

            return teams;
        }
    }
}
