using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class nameGenerator : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(setName());
    }
    IEnumerator setName() {
        yield return new WaitForSeconds(0.1f);

        gameObject.name = RandomName();
    }


    public static string RandomName()
    {
        #region MaleFirstNames
        string TEMP_male_firstNames_data = "Jacob Ethan Michael Jayden William Alexander Noah Daniel Aiden Anthony Joshua Mason Christopher Andrew " +
        "David Matthew Logan Elijah James Joseph Gabriel Benjamin Ryan Samuel Jackson John Nathan Jonathan Christian Liam Dylan Landon " +
        "Caleb Tyler Lucas Evan Gavin Nicholas Isaac Brayden Luke Angel Brandon Jack Isaiah Jordan Owen Carter Connor Justin Jose Jeremiah " +
        "Julian Robert Aaron Adrian Wyatt Kevin Hunter Cameron Zachary Thomas Charles Austin Eli Chase Henry Sebastian Jason Levi Xavier " +
        "Ian Colton Dominic Juan Cooper Josiah Luis Ayden Carson Adam Nathaniel Brody Tristan Diego Parker Blake Oliver Cole Carlos Jaden " +
        "Alex Aidan Eric Hayden Bryan Max Jaxon Brian Bentley Alejandro Sean Nolan Riley Kaden Kyle Micah Vincent Antonio Colin " +
        "Bryce Miguel Giovanni Timothy Jake Kaleb Steven Caden Bryson Damian Grayson Kayden Jesse Brady Ashton Richard Victor Patrick " +
        "Marcus Preston Joel Santiago Maxwell Ryder Edward Miles Hudson Asher Devin Elias Jeremy Ivan Jonah Easton Jace Oscar Collin " +
        "Peyton Leonardo Cayden Gage Eduardo Emmanuel Grant Alan Conner Cody Wesley Kenneth Mark Nicolas Malachi George Seth Kaiden " +
        "Trevor Jorge Derek Jude Braxton Jaxson Sawyer Jaiden Omar Tanner Travis Paul Camden Maddox Andres Cristian Rylan Josue Roman " +
        "Bradley Axel Fernando Garrett Javier Damien Peter Leo Abraham Ricardo Francisco Lincoln Erick Drake Shane Cesar Stephen Jaylen " +
        "Tucker Kai Landen Braden Mario Edwin Avery Manuel Trenton Ezekiel Kingston Calvin Edgar Johnathan Donovan Alexis Israel Mateo " +
        "Silas Jeffrey Weston Raymond Hector Spencer Andre Brendan Zion Griffin Lukas Maximus Harrison Andy Braylon Tyson Shawn Sergio " +
        "Zane Emiliano Jared Ezra Charlie Keegan Chance Drew Troy Greyson Corbin Simon Clayton Myles Xander Dante Erik Rafael Martin " +
        "Dominick Dalton Cash Skyler Theodore Marco Caiden Johnny Ty Gregory Kyler Roberto Brennan Luca Emmett Kameron Declan Quinn " +
        "Jameson Amir Bennett Colby Pedro Emanuel Malik Graham Dean Jasper Everett Aden Dawson Angelo Reid Abel Dakota Zander Paxton " +
        "Ruben Judah Jayce Jakob Finn Elliot Frank Lane Fabian Dillon Brock Derrick Emilio Joaquin Marcos Ryker Anderson Grady Devon " +
        "Elliott Holden Amari Dallas Corey Danny Cruz Lorenzo Allen Trey Leland Armando Rowan Taylor Cade Colt Felix Adan Jayson Tristen " +
        "Julius Raul Braydon Zayden Julio Nehemiah Darius Ronald Louis Trent Keith Payton Enrique Jax Randy Scott Desmond Gerardo Jett " +
        "Dustin Phillip Beckett Ali Romeo Kellen Cohen Pablo Ismael Jaime Brycen Larry Kellan Keaton Gunner Braylen Brayan Landyn Walter " +
        "Jimmy Marshall Beau Saul Donald Esteban Karson Reed Phoenix Brenden Tony Kade Jamari Jerry Mitchell Colten Arthur Brett Dennis " +
        "Rocco Jalen Tate Chris Quentin Titus Casey Brooks Izaiah Mathew King Philip Zackary Darren Russell Gael Albert Braeden Dane " +
        "Gustavo Kolton Cullen Jay Rodrigo Alberto Leon Alec Damon Arturo Waylon Milo Davis Walker Moises Kobe Curtis Matteo August " +
        "Mauricio Marvin Emerson Maximilian Reece Orlando River Bryant Issac Yahir Uriel Hugo Mohamed Enzo Karter Lance Porter Maurice " +
        "Leonel Zachariah Ricky Joe Johan Nikolas Dexter Jonas Justice Knox Lawrence Salvador Alfredo Gideon Maximiliano Nickolas Talon " +
        "Byron Orion Solomon Braiden Alijah Kristopher Rhys Gary Jacoby Davion Jamarion Pierce Sam Cason Noel Ramon Kason Mekhi Shaun " +
        "Warren Douglas Ernesto Ibrahim Armani Cyrus Quinton Isaias Reese Jaydon Ryland Terry Frederick Chandler Jamison Deandre Dorian " +
        "Khalil Ari Franklin Maverick Amare Muhammad Ronan London Eddie Moses Roger Aldo Nasir Demetrius Adriel Brodie Kelvin Morgan " +
        "Tobias Ahmad Keagan Prince Trace Alvin Giovani Kendrick Malcolm Skylar Conor Camron Abram Jonathon Bruce Noe Quincy Rohan Ahmed " +
        "Nathanael Barrett Remington Kamari Kristian Kieran Finnegan Boston Xzavier Chad Guillermo Uriah Archer Rodney Gunnar Micheal " +
        "Ulises Bobby Aaden Kamden Roy Kane Kasen Julien Ezequiel Lucian Atticus Javon Melvin Jeffery Terrance Nelson Aarav Carl Malakai " +
        "Jadon Triston Harley Jon Kian Alonzo Cory Marc Moshe Gianni Kole Dayton Jermaine Asa Wilson Felipe Kale Terrence Nico Dominik " +
        "Tommy Kendall Cristopher Isiah Finley Tristin Cannon Mohammed Wade Kash Marlon Ariel Madden Rhett Jase Layne Memphis Allan " +
        "Jamal Nash Jessie Joey Reginald Giovanny Lawson Zaiden Ace Korbin Rashad Will Urijah Billy Aron Brennen Branden Leonard Rene " +
        "Kenny Tomas Willie Darian Kody Brendon Aydan Alonso Blaine Arjun Raiden Layton Marquis Sincere Terrell Channing Chace Iker " +
        "Mohammad Jordyn Messiah Omari Santino Sullivan Brent Raphael Deshawn Elisha Harry Luciano Jefferson Jaylin Ray Yandel Aydin " +
        "Craig Tristian Zechariah Bently Francis Toby Tripp Kylan Semaj Alessandro Alexzander Lee Ronnie Gerald Dwayne Jadiel Javion " +
        "Markus Kolby Neil Stanley Makai Davin Teagan Cale Harper Callen Ben Kaeden Clark Jamie Damarion Davian Deacon Jairo Kareem " +
        "Damion Jamir Aidyn Lamar Duncan Matias Rex Jaeden Jasiah Jorden Vicente Aryan Case Tyrone Yusuf Gavyn Lewis Rogelio Zayne " +
        "Giancarlo Osvaldo Rolando Camren Luka Rylee Cedric Jensen Soren Darwin Draven Maxim Ellis Nikolai Bradyn Mathias Zackery Zavier " +
        "Emery Brantley Rudy Trevon Alfonso Beckham Darrell Harold Jerome Daxton Royce Jaylon Rory Rodolfo Tatum Bruno Sterling Gauge Van " +
        "Hamza Ayaan Rayan Zachery Keenan Jagger Heath Jovani Killian Dax Junior Misael Roland Ramiro Vance Alvaro Bode Conrad Eugene " +
        "Augustus Carmelo Adrien Kamron Gilberto Johnathon Kolten Wayne Zain Quintin Steve Tyrell Niko Antoine Hassan Jean Coleman Elian " +
        "Frankie Valentin Adonis Jamar Jaxton Kymani Bronson Clay Freddy Jeramiah Kayson Hank Abdiel Efrain Leandro Yosef Aditya Ean " +
        "Konnor Sage Samir Todd Lyric Deven Derick Jovanni Valentino Demarcus Ishaan Konner Kyson Deangelo Matthias Maximo Sidney " +
        "Benson Dilan Gilbert Kyron Xavi Bo Sylas Fisher Marcel Franco Jaron Alden Agustin Bentlee Malaki Westin Cael Jerimiah Randall " +
        "Blaze Branson Brogan Callum Dominique Justus Krish Rey Marcelo Ronin Odin Camryn Jair Izayah Brice Jabari Mayson Isai Tyree " +
        "Mike Samson Stefan Devan Emmitt Fletcher Jaidyn Remy Casen Houston Santos Seamus Jedidiah Major Vincenzo Gaige Winston Aedan " +
        "Deon Jaycob Kamryn Quinten Darnell Jaxen Deegan Landry Humberto Jadyn Salvatore Aarush Edison Kadyn Abdullah Alfred Ameer " +
        "Carsen Jaydin Lionel Howard Davon Eden Trystan Zaire Johann Antwan Bodhi Jayvion Marley Theo Bridger Donte Lennon Irvin Yael " +
        "Jencarlos Arnav Devyn Ernest Ignacio Leighton Leonidas Octavio Rayden Hezekiah Ross Hayes Lennox Nigel Vaughn Anders Keon " +
        "Dario Leroy Cortez Darryl Jakobe Koen Darien Haiden Legend Tyrese Zaid Dangelo Maxx Pierre Camdyn Chaim Damari Sonny Antony " +
        "Blaise Cain Pranav Roderick Yadiel Eliot Hugh Broderick Lathan Makhi Ronaldo Ralph Zack Kael Keyon Kingsley Talan Yair " +
        "Demarion Gibson Reagan Cristofer Daylen Jordon Dashawn Masen Clarence Dillan Kadin Rowen Thaddeus Yousef Clinton Sheldon " +
        "Slade Joziah Keshawn Menachem Bailey Camilo Destin Jaquan Jaydan Crew";
        #endregion
        #region LastNames
        string TEMP_male_lastNames_data = "Smith Babicka Johnson Williams Jones Brown Davis Miller Wilson Moore Taylor Anderson Thomas Jackson " +
        "White Harris Martin Thompson Garcia Martinez Robinson Clark Rodriguez Lewis Lee Walker Hall Allen Young Hernandez King " +
        "Wright Lopez Hill Scott Green Adams Baker Gonzalez Nelson Carter Mitchell Perez Roberts Turner Phillips Campbell Parker " +
        "Evans Edwards Collins Stewart Sanchez Morris Rogers Reed Cook Morgan Bell Murphy Bailey Rivera Cooper Richardson Cox Howard " +
        "Ward Torres Peterson Gray Ramirez James Watson Brooks Kelly Sanders Price Bennett Wood Barnes Ross Henderson Coleman " +
        "Jenkins Perry Powell Long Patterson Hughes Flores Washington Butler Simmons Foster Gonzales Bryant Alexander Russell " +
        "Griffin Diaz Hayes Myers Ford Hamilton Graham Sullivan Wallace Woods Cole West Jordan Owens Reynolds Fisher Ellis " +
        "Harrison Gibson Mcdonald Cruz Marshall Ortiz Gomez Murray Freeman Wells Webb Simpson Stevens Tucker Porter Hunter Hicks " +
        "Crawford Henry Boyd Mason Morales Kennedy Warren Dixon Ramos Reyes Burns Gordon Shaw Holmes Rice Robertson Hunt Black " +
        "Daniels Palmer Mills Nichols Grant Knight Ferguson Rose Stone Hawkins Dunn Perkins Hudson Spencer Gardner Stephens Payne " +
        "Pierce Berry Matthews Arnold Wagner Willis Ray Watkins Olson Carroll Duncan Snyder Hart Cunningham Bradley Lane Andrews " +
        "Ruiz Harper Fox Riley Armstrong Carpenter Weaver Greene Lawrence Elliott Chavez Sims Austin Peters Kelley Franklin Lawson " +
        "Fields Gutierrez Ryan Schmidt Carr Vasquez Castillo Wheeler Chapman Oliver Montgomery Richards Williamson Johnston " +
        "Banks Meyer Bishop Mccoy Howell Alvarez Morrison Hansen Fernandez Garza Harvey Little Burton Stanley Nguyen George " +
        "Jacobs Reid Kim Fuller Lynch Dean Gilbert Garrett Romero Welch Larson Frazier Burke Hanson Day Mendoza Moreno Bowman " +
        "Medina Fowler Brewer Hoffman Carlson Silva Pearson Holland Douglas Fleming Jensen Vargas Byrd Davidson Hopkins May " +
        "Terry Herrera Wade Soto Walters Curtis Neal Caldwell Lowe Jennings Barnett Graves Jimenez Horton Shelton Barrett Castro " +
        "Sutton Gregory Mckinney Lucas Miles Craig Rodriquez Chambers Holt Lambert Fletcher Watts Bates Hale Rhodes Pena Beck " +
        "Newman Haynes Mcdaniel Mendez Bush Vaughn Parks Dawson Santiago Norris Hardy Love Steele Curry Powers Schultz Barker " +
        "Guzman Page Munoz Ball Keller Chandler Weber Leonard Walsh Lyons Ramsey Wolfe Schneider Mullins Benson Sharp Bowen Daniel " +
        "Barber Cummings Hines Baldwin Griffith Valdez Hubbard Salazar Reeves Warner Stevenson Burgess Santos Tate Cross Garner " +
        "Mann Mack Moss Thornton Dennis Mcgee Farmer Delgado Aguilar Vega Glover Manning Cohen Harmon Rodgers Robbins Newton Todd " +
        "Blair Higgins Ingram Reese Cannon Strickland Townsend Potter Goodwin Walton Rowe Hampton Ortega Patton Swanson Joseph " +
        "Francis Goodman Maldonado Yates Becker Erickson Hodges Rios Conner Adkins Webster Norman Malone Hammond Flowers Cobb " +
        "Moody Quinn Blake Maxwell Pope Floyd Osborne Paul Mccarthy Guerrero Lindsey Estrada Sandoval Gibbs Tyler Gross Fitzgerald " +
        "Stokes Doyle Sherman Saunders Wise Colon Gill Alvarado Greer Padilla Simon Waters Nunez Ballard Schwartz Mcbride " +
        "Houston Christensen Klein Pratt Briggs Parsons Mclaughlin Zimmerman French Buchanan Moran Copeland Roy Pittman Brady " +
        "Mccormick Holloway Brock Poole Frank Logan Owen Bass Marsh Drake Wong Jefferson Park Morton Abbott Sparks Patrick " +
        "Norton Huff Clayton Massey Lloyd Figueroa Carson Bowers Roberson Barton Tran Lamb Harrington Casey Boone Cortez Clarke " +
        "Mathis Singleton Wilkins Cain Bryan Underwood Hogan Mckenzie Collier Luna Phelps Mcguire Allison Bridges Wilkerson " +
        "Nash Summers Atkins Wilcox Pitts Conley Marquez Burnett Richard Cochran Chase Davenport Hood Gates Clay Ayala Sawyer " +
        "Roman Vazquez Dickerson Hodge Acosta Flynn Espinoza Nicholson Monroe Wolf Morrow Kirk Randall Anthony Whitaker Skinner " +
        "Ware Molina Kirby Huffman Bradford Charles Gilmore Dominguez Bruce Lang Combs Kramer Heath Hancock Gallagher Gaines " +
        "Shaffer Short Wiggins Mathews Mcclain Fischer Wall Small Melton Hensley Bond Dyer Cameron Grimes Contreras Christian " +
        "Wyatt Baxter Snow Mosley Shepherd Larsen Hoover Beasley Glenn Petersen Whitehead Meyers Keith Garrison Vincent Shields " +
        "Horn Savage Olsen Schroeder Hartman Woodard Mueller Kemp Deleon Booth Patel Calhoun Wiley Eaton Cline Navarro Harrell " +
        "Lester Humphrey Parrish Duran Hutchinson Hess Dorsey Bullock Robles Beard Dalton Avila Vance Rich Blackwell York Johns " +
        "Blankenship Trevino Salinas Campos Pruitt Moses Callahan Golden Montoya Hardin Guerra Mcdowell Carey Stafford Gallegos " +
        "Henson Wilkinson Booker Merritt Miranda Atkinson Orr Decker Hobbs Preston Tanner Knox Pacheco Stephenson Glass Rojas " +
        "Serrano Marks Hickman English Sweeney Strong Prince Mcclure Conway Walter Roth Maynard Farrell Lowery Hurst Nixon Weiss " +
        "Trujillo Ellison Sloan Juarez Winters Mclean Randolph Leon Boyer Villarreal Mccall Gentry Carrillo Kent Ayers Lara Shannon " +
        "Sexton Pace Hull Leblanc Browning Velasquez Leach Chang House Sellers Herring Noble Foley Bartlett Mercado Landry Durham " +
        "Walls Barr Mckee Bauer Rivers Everett Bradshaw Pugh Velez Rush Estes Dodson Morse Sheppard Weeks Camacho Bean Barron " +
        "Livingston Middleton Spears Branch Blevins Chen Kerr Mcconnell Hatfield Harding Ashley Solis Herman Frost Giles Blackburn " +
        "William Pennington Woodward Finley Mcintosh Koch Best Solomon Mccullough Dudley Nolan Blanchard Rivas Brennan Mejia " +
        "Kane Benton Joyce Buckley Haley Valentine Maddox Russo Mcknight Buck Moon Mcmillan Crosby Berg Dotson Mays Roach Church " +
        "Chan Richmond Meadows Faulkner Knapp Kline Barry Ochoa Jacobson Gay Avery Hendricks Horne Shepard Hebert Cherry Cardenas " +
        "Mcintyre Whitney Waller Holman Donaldson Cantu Terrell Morin Gillespie Fuentes Tillman Sanford Bentley Peck Key Salas " +
        "Rollins Gamble Dickson Battle Santana Cabrera Cervantes Howe Hinton Hurley Spence Zamora Yang Mcneil Suarez Case Petty " +
        "Gould Mcfarland Sampson Carver Bray Rosario Macdonald Stout Hester Melendez Dillon Farley Hopper Galloway Potts Bernard " +
        "Joyner Stein Aguirre Osborn Mercer Bender Franco Rowland Sykes Benjamin Travis Pickett Crane Sears Mayo Dunlap Hayden " +
        "Wilder Mckay Coffey Mccarty Ewing Cooley Vaughan Bonner Cotton Holder Stark Ferrell Cantrell Fulton Lynn Lott Calderon " +
        "Rosa Pollard Hooper Burch Mullen Fry Riddle Levy David Duke Guy Michael Britt Frederick Daugherty Berger Dillard Alston " +
        "Jarvis Frye Riggs Chaney Odom Duffy Fitzpatrick Valenzuela Merrill Mayer Alford Mcpherson Acevedo Donovan Barrera Albert " +
        "Cote Reilly Compton Raymond Mooney Mcgowan Craft Cleveland Clemons Wynn Nielsen Baird Stanton Snider Rosales Bright Witt " +
        "Stuart Hays Holden Rutledge Kinney Clements Castaneda Slater Hahn Emerson Conrad Burks Delaney Pate Lancaster Sweet " +
        "Justice Tyson Sharpe Whitfield Talley Macias Irwin Burris Ratliff Mccray Madden Kaufman Beach Goff Cash Bolton Mcfadden " +
        "Levine Good Byers Kirkland Kidd Workman Carney Dale Mcleod Holcomb England Finch Head Burt Hendrix Sosa Haney Franks " +
        "Sargent Nieves Downs Rasmussen Bird Hewitt Lindsay Delacruz Vinson Hyde Forbes Gilliam Guthrie Wooten Huber " +
        "Barlow Boyle Mcmahon Buckner Rocha Puckett Langley Knowles Cooke Velazquez Whitley Noel Vang";
        #endregion
        string[] TEMP_male_firstNames = TEMP_male_firstNames_data.Split(' ');
        string[] TEMP_male_lastNames = TEMP_male_lastNames_data.Split(' ');
        return TEMP_male_firstNames[Random.Range(0, TEMP_male_firstNames.Length - 1)] + " " + TEMP_male_lastNames[Random.Range(0, TEMP_male_lastNames.Length - 1)];
    }





    /*
        public List<string> firstnames;
        public List<string> surnames;


        // Use this for initialization
        void Start()
        {
            TextAsset nameText = Resources.Load<TextAsset>("Names");

            string[] lines = nameText.text.Split("\n"[0]);

            bool addingFirstnames = true;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "FirstNames:")
                {
                    addingFirstnames = true;
                    Debug.Log("adding first names");
                    continue;
                }

                if (lines[i] == "SurNames:")
                {
                    addingFirstnames = false;
                    Debug.Log("adding surnames");
                    continue;
                }

                if (lines[i] != "")
                {
                    if (addingFirstnames)
                    {
                        firstnames.Add(lines[i]);
                    }
                    else
                    {
                        surnames.Add(lines[i]);
                    }

                }


            }
        }

        string generateName()
        {
            string firstname;
            string surname;

            firstname = firstnames[Random.Range(0, firstnames.Count)];
            surname = surnames[Random.Range(0, surnames.Count)];

            string returnName = firstname + " " + surname;

            return returnName;

        }

        // Update is called once per frame
        void Update()
        {
            //gameObject.name = generateName();
            Debug.Log(generateName());
        }*/

}