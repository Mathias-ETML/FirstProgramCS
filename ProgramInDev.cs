// problème du cavalier : si je reste appuyé sur par exemple : la flèche de droite, et que je fonce dans un mur, 
// le sleep ne vas pas s'effectuer corrêctement et va répéter le pressement de la touche au lieu de tout arrêter et de continuer
// https://docs.microsoft.com/fr-fr/dotnet/csharp/programming-guide/concepts/async/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProjetCavalierFinal
{
    class Program
    {
        /// <summary>
        /// Variables globales, norme Deschamps
        /// </summary>

        private static sbyte G_sbyteUser = 0;
        private static char G_charTbl = ' ';

        private static sbyte G_sbyteXUser = 1;
        private static sbyte G_sbyteYUser = 1;

        private static byte G_byteX = 0;
        private static byte G_byteY = 0;

        private static char[,] G_Tab_charCavalier;
        private static char[,] G_Tab_charCavalierAnswer;        

        /// <summary>
        /// Reset : Reset du cavalier, donc des variables, tableaux, char, console
        /// Problème : je n'arrivais pas a reflatten le tableau de comparaison
        /// Resolution : faire du cavalier un bool, si le bool est true alors reflatten le tableau
        /// Succès : oui
        /// Optimisation : ok
        /// </summary>

        private static char[] Reset()
        {
            // clear de la console
            Console.Clear();

            // reset couleur console
            Console.ForegroundColor = ConsoleColor.Gray;

            // reset des variables
            // reset position user
            G_sbyteXUser = 1;
            G_sbyteYUser = 1;

            // reset pour ecrire les tableaux
            G_byteX = 0;
            G_byteY = 0;

            // oui je sais que les tableaux avec des valeurs nulls c'est pas bien mais il faut bien que je les réinitialise
            G_Tab_charCavalier = null;
            G_Tab_charCavalierAnswer = null;

            // reset taille tbl user
            G_sbyteUser = 0;

            // call le programm InputUser, se réferer au static void InputUser() pour le code
            InputUser();

            // call le programm tableau, se réferer au static void Tableau() pour le code
            return Tableau();
        }

        /// <summary>
        /// Input user : permet d'entrer à l'user la taille ( longeur largeur ) du cavalier
        /// Succès : Oui
        /// Optimisation : ok
        /// </summary>

        private static void InputUser()
        {
            // début du code
            Console.Clear();

            // reset couleur console
            Console.ForegroundColor = ConsoleColor.Gray;

            // titre
            Console.Write("*******************************\n" +
                          "*        Projet cavalier      *\n" +
                          "*******************************\n");

            // demande la largeur du cavalier
            for (; G_sbyteUser < 7 || G_sbyteUser > 30;)
            {
                // demande l'input user
                Console.Write("Entrez la largeur du cavalier (min : 7 / max : 30) : ");

                // oui je sais les try catch c'est pas bien mais c'est au cas ou qqn met 128 par exemple
                try
                {
                    // converti l'input user
                    G_sbyteUser = Convert.ToSByte(Console.ReadLine());
                }
                // catch si l'user ecrit un nombre > 127
                catch (OverflowException)
                {
                    G_sbyteUser = 0;
                }
                // catch si l'user ecrit un caractère autre que un nombre
                catch (FormatException)
                {

                }
            }

            // clear console pour laisser apparaitre seulement le tableau
            Console.Clear();
        }

        /// <summary>
        /// Tableau : Permet d'ecrire le tableau principal ou le joueur va se déplacer et le tableau ou il peut voir ce que il doit encore completer
        /// Succès : Oui
        /// Optimisation : peut faire mieux
        /// </summary>

        private static char[] Tableau()
        {
            // création du tableau answer avec l'input user
            G_Tab_charCavalierAnswer = new char[G_sbyteUser, G_sbyteUser];

            // création tableau user avec l'input user
            G_Tab_charCavalier = new char[G_sbyteUser, G_sbyteUser];

            // début boucle for Y, pour ecrire verticallement les cavalies
            for (G_byteY = 0; G_byteY != G_sbyteUser; G_byteY++)
            {
                // début boucle for Y, pour ecrire honrizontallement les cavalies
                for (G_byteX = 0; G_byteX != G_sbyteUser; G_byteX++)
                {
                    // si G_byteX ou G_byteY = a 0 ou a sbyteUser -1, donc c'est une bordure
                    if (G_byteX == 0 || G_byteY == 0 || G_byteX == G_sbyteUser - 1 || G_byteY == G_sbyteUser - 1)
                    {
                        // regarde si la position actuelle de la boucle for est autre que la bordure tout en haut et tout en bas
                        if (G_byteX == 0 && G_byteY != 0 && G_byteY != G_sbyteUser - 1 || G_byteX == G_sbyteUser - 1 && G_byteY != 0 && G_byteY != G_sbyteUser - 1)
                        {
                            G_charTbl = '║';
                        }
                        // regarde si la position actuelle de la boucle for est autre que la bordure à gauche ou a droite
                        else if (G_byteY == 0 && G_byteX != 0 && G_byteX != G_sbyteUser - 1 || G_byteY == G_sbyteUser - 1 && G_byteX != 0 && G_byteX != G_sbyteUser - 1)
                        {
                            G_charTbl = '═';
                        }
                        // regarde si c'est le coin en haut a droite
                        else if (G_byteX == 0 && G_byteY == 0)
                        {
                            G_charTbl = '╔';
                        }
                        // regarde si c'est le coin en haut a gauche
                        else if (G_byteX == G_sbyteUser - 1 && G_byteY == 0)
                        {
                            G_charTbl = '╗';
                        }
                        // regarde si c'est le coin en bas a droite
                        else if (G_byteX == 0 && G_byteY == G_sbyteUser - 1)
                        {
                            G_charTbl = '╚';
                        }
                        // regarde si c'est le coin en bas a gauche
                        else if (G_byteX == G_sbyteUser - 1 && G_byteY == G_sbyteUser - 1)
                        {
                            G_charTbl = '╝';
                        }

                        // met la bordure en bleu vu que c'est une bordure
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    // si non alors c'est le millieu du tableau
                    else
                    {
                        // met la couleur des caratères a ecrire en rouge, car c'est une croix
                        Console.ForegroundColor = ConsoleColor.Red;

                        // change le caratère actuelle en croix
                        G_charTbl = 'x';
                    }

                    // ecrit le caratère actuelle dans le tableau principale
                    G_Tab_charCavalier[G_byteY, G_byteX] = G_charTbl;

                    // ecrit le caratère actuelle dans le tableau secondaire
                    G_Tab_charCavalierAnswer[G_byteY, G_byteX] = G_charTbl;

                    // si le caratère actuelle est un X, alors va ecrire dans le tableau de réponse un O, vu que on doit remplire le tableau user avec des O
                    if (G_charTbl == 'x')
                    {
                        // ecrit un O dans le tableau de réponse
                        G_Tab_charCavalierAnswer[G_byteY, G_byteX] = 'O';
                    }

                    // set le curseur pour ecrire le tableau principal
                    Console.SetCursorPosition(G_byteX, G_byteY);

                    // ecrit le caractère séléctionné par l'algoritme
                    Console.Write(G_charTbl);

                    // set le curseur pour ecrire le tableau secondaire
                    Console.SetCursorPosition(G_byteX + G_sbyteUser + 1, G_byteY);

                    // ecrit le caractère séléctionné par l'algoritme
                    Console.Write(G_charTbl);

                } // fin boucle for X
            } // fin boucle for Y

            // initalisation pion
            G_Tab_charCavalier[G_sbyteXUser, G_sbyteYUser] = 'O';

            // change la couleur en gris
            Console.ForegroundColor = ConsoleColor.Gray;

            // met le curseur en dehor du tableau
            Console.SetCursorPosition(5, G_sbyteYUser + G_sbyteUser);

            // ecrit comment leave le jeu
            Console.Write("Appuyze sur Escape pour quitter le jeu");

            // met le curseur en dehor du tableau avec y + 1
            Console.SetCursorPosition(5, G_sbyteYUser + G_sbyteUser + 1);

            // ecrit comment reset le jeu
            Console.Write("Appuyze sur 'r' pour reset le cavalier");
            
            return G_Tab_charCavalierAnswer.Cast<char>().ToArray();

        } // fin void static Tableau()

        /// <summary>
        /// Utilité : je sais pas
        /// ErreurTbl : permet de dire a l'user que il c'est déplacé en dehors du tableau
        /// Optimisation : ok
        /// Documentation : https://zestedesavoir.com/tutoriels/884/lasynchrone-et-le-multithread-en-net/ || https://docs.microsoft.com/fr-fr/dotnet/csharp/programming-guide/concepts/async/
        /// </summary>

        private static async Task ErreurTbl()
        {
            // se met en dehor du tableau
            Console.SetCursorPosition(0, G_sbyteUser + 4);

            // change la couleur de la console
            Console.ForegroundColor = ConsoleColor.Red;

            // ecrit le message d'erreur
            Console.Write("\n\nVous ne pouvez pas vous déplacer en dehors du tableau");

            // attends que le sleep se termine
            await Task.Factory.StartNew(() => Thread.Sleep(2000));

            // met le curseur pour effacer le message
            Console.SetCursorPosition(0, G_sbyteUser + 6);

            // crée un nouveau string de  la longeur de 
            Console.Write(new string(' ', Console.WindowWidth));

        } // fin void static ErreurTbl()

        /// <summary>
        /// CheckChar : permet de voir si le pion est a coté d'une bordure
        /// Fonctionnement : Marche par élémination || Regarde si il se trouve en haut ou a gauche, si non alors en bas ou a droite
        /// Succès : Oui
        /// Optimisation : 2ème version du CheckChar, se référer a la capture d'ecran " Avant.png "                  
        /// PS : ne pas oublier d'inverser la position cursuer ( marche en x y ) et le tableau ( marche en y x )
        /// </summary>

        private static void CheckChar()
        {
            // check si il est a coté d'une bordure
            if (G_sbyteXUser == 1 || G_sbyteYUser == 1 || G_sbyteXUser == G_sbyteUser - 2 || G_sbyteYUser == G_sbyteUser - 2)
            {
                // change la couleur d'ecriture des caractères en bleu vu que c'est une bordure
                Console.ForegroundColor = ConsoleColor.Blue;

                // regarde si il est dans la partie en haut ou a droite du tableau
                if (G_sbyteXUser == 1 || G_sbyteYUser == 1)
                {
                    // regarde si il est sur la bordure du haut
                    if (G_sbyteYUser == 1 && G_sbyteXUser != 1 && G_sbyteXUser != G_sbyteUser - 2)
                    {
                        // met le curseur sur la bordure du haut
                        Console.SetCursorPosition(G_sbyteXUser - 1, G_sbyteYUser - 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╦');

                        // met le curseur sur la bordure du haut
                        Console.SetCursorPosition(G_sbyteXUser + 1, G_sbyteYUser - 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╦');
                    }
                    // si non alors bordure de gauche
                    else if (G_sbyteXUser == 1 && G_sbyteYUser != 1 && G_sbyteYUser != G_sbyteUser - 2)
                    {
                        // met le curseur sur la bordure de gauche
                        Console.SetCursorPosition(G_sbyteXUser - 1, G_sbyteYUser + 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╠');

                        // met le curseur sur la bordure de gauche
                        Console.SetCursorPosition(G_sbyteXUser - 1, G_sbyteYUser - 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╠');
                    }
                    // regarde si il est dans le coin en bas a droite
                    else if (G_sbyteXUser == 1 && G_sbyteYUser == G_sbyteUser - 2)
                    {
                        // met le curseur sur la bordure de gauche
                        Console.SetCursorPosition(G_sbyteXUser - 1, G_sbyteYUser - 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╠');

                        // met le curseur sur la bordure en bas
                        Console.SetCursorPosition(G_sbyteXUser + 1, G_sbyteYUser + 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╩');
                    }
                    // regarde si il est dans le coin sur la bordure de gauche
                    else if (G_sbyteXUser == G_sbyteUser - 2)
                    {
                        // met le curseur sur la bordure du haut
                        Console.SetCursorPosition(G_sbyteXUser - 1, G_sbyteYUser - 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╦');

                        // met le curseur sur la bordure de droite
                        Console.SetCursorPosition(G_sbyteXUser + 1, G_sbyteYUser + 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╣');
                    }
                    // regarde si il est dans le coin en haut a gauche
                    else
                    {
                        // met le curseur sur la bordure du haut
                        Console.SetCursorPosition(G_sbyteXUser + 1, G_sbyteYUser - 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╦');

                        // met le curseur sur la bordure de gauche
                        Console.SetCursorPosition(G_sbyteXUser - 1, G_sbyteYUser + 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╠');
                    }
                }
                // si non alors se trouve a droite ou en bas
                else
                {
                    // regarde si il est bordure de droite
                    if (G_sbyteXUser == G_sbyteUser - 2 && G_sbyteYUser != G_sbyteUser - 2)
                    {
                        // met le curseur sur la bordure de droite
                        Console.SetCursorPosition(G_sbyteXUser + 1, G_sbyteYUser - 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╣');

                        // met le curseur sur la bordure de droite
                        Console.SetCursorPosition(G_sbyteXUser + 1, G_sbyteYUser + 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╣');
                    }
                    // si non regarde si il est sur la bordure du bas
                    else if (G_sbyteYUser == G_sbyteUser - 2 && G_sbyteXUser != G_sbyteUser - 2)
                    {
                        // met le curseur sur la bordure en bas
                        Console.SetCursorPosition(G_sbyteXUser + 1, G_sbyteYUser + 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╩');

                        // met le curseur sur la bordure en bas
                        Console.SetCursorPosition(G_sbyteXUser - 1, G_sbyteYUser + 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╩');
                    }
                    // si non alors bordure en bas a gauche
                    else
                    {
                        // met le curseur sur la bordure du bas
                        Console.SetCursorPosition(G_sbyteXUser - 1, G_sbyteYUser + 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╩');

                        // met le curseur sur la bordure de gauche
                        Console.SetCursorPosition(G_sbyteXUser + 1, G_sbyteYUser - 1);

                        // ecrit le caractère pour que la box pion et la bordure soient continue
                        Console.Write('╣');
                    }
                }
            } // si faux alors pas a coté d'une bordure
        } // fin staitc Void CheckChar()

        /// <summary>
        /// Deco Pion : permet de décorer le pion avec les bordures
        /// Fonctionnement : je prends une zone de 5x5 || Pourquoi ? : car si le pion au bord et que on se déplace, il faut que je redessine la bordure corrêctement
        /// Try Catch : pourquoi un try catch ? Si on se trouve dans un coin ou vers une bordure, je check en dehors de la porté du tableau, d'ou le try catch
        /// Succès : Oui
        /// Optimisation : a voir
        /// </summary>

        private static void DecoPion()
        {
            // ╝    ╗    ╔    ╚    ╣    ╩    ╦    ╠    ═    ║    ╬
            // tableau qui permet de décorer le pion
            char[,] tab_charCaracteres =
            {
                {'╔', '═', '╗' },
                {'║', 'O', '║' },
                {'╚', '═', '╝' }
            };

            // début boucle for Y, pour la hauteur
            for (G_byteY = 0 ; G_byteY < 5; G_byteY++)
            {
                // début boucle for X, pour la largeur
                for (G_byteX = 0 ; G_byteX < 5; G_byteX++)
                {
                    // try, pour catch les excpetion si curseur se met en dehors console ou si valeur recherché en dehors tableau
                    try
                    {
                        // regarde si XY sont dans la range pour ecrire le carré du pion
                        if (G_byteX <= 3 && G_byteX >= 1 && G_byteY != 0 && G_byteY != 4)
                        {
                            // met les caratères a ecrire en bleu vu que c'est alors une bordure
                            Console.ForegroundColor = ConsoleColor.Blue;

                            // met le curseur via la position XY - 2 ( vu que on check sur une zone de 5x5 et le pion est au millieu )
                            Console.SetCursorPosition(G_sbyteXUser + G_byteX - 2, G_sbyteYUser + G_byteY - 2);

                            // ecrit les caratères du tableau - 1 ( vu que il va de 0 à 2 )
                            Console.Write(tab_charCaracteres[G_byteY - 1, G_byteX - 1]);
                        }
                        else
                        {
                            // check si le caractère ou il se trouve est un " x "
                            if (G_Tab_charCavalier[G_sbyteYUser + G_byteY - 2, G_sbyteXUser + G_byteX - 2] == 'x')
                            {
                                // change la couleur des caractères a ecrire en rouge
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            // check si le caractère ou il se trouve est un " O ", donc une ancienne case du joueur
                            else if (G_Tab_charCavalier[G_sbyteYUser + G_byteY - 2, G_sbyteXUser + G_byteX - 2] == 'O')
                            {
                                // change la couleur des caractères a ecrire en gris
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            // si non c'est une bordure du cavalier
                            else
                            {
                                // change la couleur des caractères a ecrire en bleu
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            // met le curseur via la position XY - 2 ( vu que on check sur une zone de 5x5 et le pion est au millieu )
                            Console.SetCursorPosition(G_sbyteXUser + G_byteX - 2, G_sbyteYUser + G_byteY - 2);

                            // ecrit le carctère du tableau du joueur + gbyteY - 2 car on est dans une zone autour du pion
                            Console.Write(G_Tab_charCavalier[G_sbyteYUser + G_byteY - 2, G_sbyteXUser + G_byteX - 2]);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                    catch (IndexOutOfRangeException)
                    {

                    }                   
                } // fin boucle for X
            } // fin boucle for Y
        } // fin void static DecoPion

        /// <summary>
        /// CavalierClean : permet d'ecrire le cavalier sans les bordures du pion, donc permet de voir à l'user ce que il doit encore remplire
        /// Succès : Oui
        /// Optimisation : ok
        /// </summary>

        private static void CavalierClean()
        {
            // regarde si la position actuelle du joueur est un endroit ou il est déjà passé ou pas
            if (G_Tab_charCavalier[G_sbyteYUser, G_sbyteXUser] == 'x')
            {
                // si oui alors met la couleur d'ecriture en rouge
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
	        {
                // si non alors en gris 
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            // déplace le curseur sur le tableau clean
            Console.SetCursorPosition(G_sbyteXUser + G_sbyteUser + 1, G_sbyteYUser);

            // ecrit ce que il y a dans le tableau clean depuis le tableau joueur
            Console.Write(G_Tab_charCavalier[G_sbyteYUser, G_sbyteXUser]);
            
        } // fin static void CavalierClean

        /// <summary>
        /// CheckTbl : permet de voir si le pion est en dehors de la zone de jeu, si oui alors return la valeur entrée dans la methode
        /// Succès : Oui
        /// Optimisation : ok
        /// </summary>

        private static async Task<sbyte> checkTbl(sbyte sbytePlusMoin)
        {
            // regarde si il se trouve sur les bordures du cavalier
            if (G_sbyteXUser == 0 || G_sbyteXUser == G_sbyteUser - 1 || G_sbyteYUser == 0 || G_sbyteYUser == G_sbyteUser - 1)
            {
                // ecriture que il doit rester dans le tableau
                await ErreurTbl();

                // return la valeur entrée a additionner pour que le pion revienne dans la position d'avant
                return sbytePlusMoin;
            }
            // si non, ecrit dans le tableau ou est le pion
            else if (G_Tab_charCavalier[G_sbyteYUser, G_sbyteXUser] == 'x')
            {
                // ecrit O dans le tableau user
                G_Tab_charCavalier[G_sbyteYUser, G_sbyteXUser] = 'O';

                // return rien vu que pas d'erreur
                return 0;
            }
            // si non sa veut dire que il est déjà passé par la, alors ecrit un x
            else
            {
                // ecrit X dans le tableau user
                G_Tab_charCavalier[G_sbyteYUser, G_sbyteXUser] = 'x';

                // return rien vu que pas d'erreur
                return 0;
            }            
        } // fin static sbyte CheckTbl

        /// <summary>
        /// Cavalier : programme qui permet de déplacer le pion de l'user
        /// Problème : je n'arrivais pas reset le tableau flatten, ducoup je l'ai transformé en bool, si le bool est true alors reset le tableau a flatten
        /// Succès : Oui
        /// Optimisation : je pense c'est pas mal
        /// </summary>

        private static async Task<char[]> Cavalier()
        {
            char[] Tab_CharFlatten = G_Tab_charCavalierAnswer.Cast<char>().ToArray();

            // regarde si le pion se trouve vers une bordure
            CheckChar();

            // change l'ecriture des craratères en vert
            Console.ForegroundColor = ConsoleColor.Green;

            // met la position du curseur en fonction de la position de l'user
            Console.SetCursorPosition(G_sbyteXUser, G_sbyteYUser);

            // ecrit le pion en vert
            Console.Write('O');

            // variable utilisé dans le switch
            ConsoleKey controle = Console.ReadKey().Key;

            switch (controle)
            {
                // optimiser le code en faisant un void static pour les condition voir si le pion est en dehor du cavalier
                case ConsoleKey.LeftArrow:

                    // décrémentation vu que il va a droite
                    --G_sbyteXUser;

                    // remet le pion ou il était avant et ecrit un message d'erreur
                    G_sbyteXUser += await checkTbl(1);

                    // sort du switch
                    break;

                case ConsoleKey.RightArrow:

                    // incrémentation vu que il bouge a droite
                    ++G_sbyteXUser;

                    // remet le pion ou il était avant et ecrit un message d'erreur
                    G_sbyteXUser += await checkTbl(-1);

                    // sort du switch
                    break;

                case ConsoleKey.UpArrow:;

                    // décrémentaiton G_sbyteYUser vu que il se dirrige en haut
                    --G_sbyteYUser;

                    // remet le pion ou il était avant et ecrit un message d'erreur
                    G_sbyteYUser += await checkTbl(1);

                    // sort du switch
                    break;

                case ConsoleKey.DownArrow:

                    // incrémentation G_sbyteYUser vu que il se dirrige en bas
                    ++G_sbyteYUser;

                    // remet le pion ou il était avant et ecrit un message d'erreur
                    G_sbyteYUser += await checkTbl(-1);

                    // sort du switch
                    break;

                case ConsoleKey.Escape:

                    // quitte le programme
                    Environment.Exit(0);

                    // sort du switch
                    break;

                case ConsoleKey.R:

                    // reset le cavalier, se réferer au static void Reset() pour le code
                    // return le tableau flatten
                    Tab_CharFlatten = Reset();

                    // sort du switch
                    break;
                
                // si aucune de ces actions on été effectuées, alors sa veut dire que l'user a appuuyé sur une touche autre du clavier
                default:

                    // met le curseur en dehor du cavalier
                    Console.SetCursorPosition(G_sbyteUser - 2, G_sbyteUser + 4);

                    // set la console en rouge
                    Console.ForegroundColor = ConsoleColor.Red;

                    // averti l'user d'use les touches du clavier
                    Console.Write("Veuillez appuyer sur une des flèches du clavier");

                    // sort du switch
                    break;
            } // fin du switch

            // permet de décorer le pion
            DecoPion();

            // return le tableau flatten actuelle ou celui du reset
            return Tab_CharFlatten;
        } // fin static bool cavalier

        /// <summary>
        /// Main : Comme son nom l'indique, programme principale qui permet d'appeler les autres methodes pour le cavalier
        /// Succès : Oui
        /// Optimisation : je pense il y a moyen de faire mieux
        /// </summary>

        private static void Main(string[] agrs)
        {
            // permet de run mon cavalier en mode asynchrone
            Task.Run(async () =>
            {
                // enlève la visibilité du curseur
                Console.CursorVisible = false;

                // call le programm InputUser, se réferer au static void InputUser() pour le code
                InputUser();

                // call le programm tableau, se réferer au static void Tableau() pour le code
                Tableau();

                // permet de décorer le pion
                DecoPion();

                // call le programm cavalier, se réferer au static void CavalierClean() pour le code
                CavalierClean();

                // flatten le tableau qui permet de comparer si le joueur l'a rempli de 'O'
                // bug, n'arrive pas a reset le tableau flattent
                char[] tab_FlattenTblAnswer = G_Tab_charCavalierAnswer.Cast<char>().ToArray();

                // crée une première version du tableau ou le pion se trouve
                char[] tab_FlattenPion = G_Tab_charCavalier.Cast<char>().ToArray();

                // cavalier, recommence tant que le chat restart = o
                for (; !tab_FlattenPion.SequenceEqual(tab_FlattenTblAnswer);)
                {
                    // call le programm cavalier, se réferer au static void CavalierClean() pour le code
                    CavalierClean();

                    // call le programm cavalier, se réferer au static void Cavalier() pour le code
                    tab_FlattenTblAnswer = await Cavalier();

                    // reflaten le tableau a chaque fois vu que on se déplace
                    tab_FlattenPion = G_Tab_charCavalier.Cast<char>().ToArray();
                }
                // clear de la console
                Console.Clear();

                // change la couleur de la console en vert
                Console.ForegroundColor = ConsoleColor.Green;

                // ecrit que on a fini
                Console.Write("Félicitation, vous avez gagné");

                // sleep de 3 sec pour que l'user puisse voir que il a fini
                Thread.Sleep(3000);

                // je vais vous mentir mais j'ai 0 idée de ce que sa fait
            }).GetAwaiter().GetResult();
        } // fin static void Main ( jeu cavalier )       
    } // fin class Programm
} // fin Namespace ProjetCavalier final
// fin ProjetCavalierFinal le 11.03.2020
// le projet est foncitonnel, mais loin d'être commenté assez et optimisé de mon point de vue


// fin optimisation et commentaire du projet le 15.03.2020
// j'ai optimisé les parties du code que je trouvais nul et mis des commentaires partout ( litéralement )
