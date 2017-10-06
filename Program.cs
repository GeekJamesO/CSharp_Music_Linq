using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

                System.Console.WriteLine("There is only one artist in this collection from Mount Vernon, what is their name and age?");
                foreach (Artist musician in Artists.Where(a => a.Hometown == "Mount Vernon"))
                {
                System.Console.WriteLine($"     {musician.ArtistName} and is age {musician.Age}" );
                }
                System.Console.WriteLine("Who is the youngest artist in our collection of artists?");
                var person = Artists.OrderBy(a => a.Age).First();
                {
                    System.Console.WriteLine($"     {person.ArtistName} at age {person.Age}");
                }
                System.Console.WriteLine("The artists whom have the real name containing William:");
                foreach (var person2 in Artists.Where(a => a.RealName.Contains("William")).OrderBy(a => a.RealName))
                {
                    System.Console.WriteLine($"     {person2.RealName} as {person2.ArtistName}");
                }
                System.Console.WriteLine("Display all groups with names less than 8 characters in length.");
                foreach (var group in Groups.Where(g => g.GroupName.Length < 8).OrderBy(g => g.GroupName.Length))
                {
                    System.Console.WriteLine($"     {group.GroupName}");
                }
                System.Console.WriteLine("Display the 3 oldest artist from Atlanta.");
                foreach (var person2 in Artists.Where(a => a.Hometown == "Atlanta").OrderByDescending(a => a.Age).Take(3))
                {
                    System.Console.WriteLine($"     {person2.RealName} from {person2.Hometown} is {person2.Age} Years old");
                }
                System.Console.WriteLine("(Optional) Display the Group Name of all groups that have members that are not from New York City");
                var NYC_GroupsIds = Groups.Join(inner: Artists
                                , outerKeySelector: grp => grp.Id
                                , innerKeySelector: art => art.GroupId
                                , resultSelector: (grp, art) => new { grp.Id, art.Hometown })
                                    .Where(Artist => Artist.Hometown == "New York City").Distinct().Select(g => g.Id);
                var NonNYC_Groups = Groups.Where(g => (false == NYC_GroupsIds.Contains(g.Id))).OrderBy(g => g.GroupName);
                foreach (var k in NonNYC_Groups)
                {
                    System.Console.WriteLine($"     {k.GroupName}");
                }

                System.Console.WriteLine("(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'");                
                var WuTangClanMembers = Groups.Join(inner: Artists
                , outerKeySelector: grp => grp.Id
                , innerKeySelector: art => art.GroupId
                , resultSelector: (grp, art) => new { grp.GroupName, art.RealName, art.ArtistName })
                    .Where(grp => grp.GroupName == "Wu-Tang Clan").OrderBy(k => k.RealName);
                foreach (var musician in WuTangClanMembers)
                {
                    System.Console.WriteLine($"     {musician.RealName}  ==> {musician.ArtistName}");
                }
        }
    }
}

