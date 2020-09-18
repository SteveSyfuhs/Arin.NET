using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Arin.NET.Client;
using Arin.NET.Entities;

namespace ArinConsoleClient
{
    class Program
    {
        private static bool DisplayDetails = false;

        static async Task Main(string[] args)
        {
            string addr = null;

            if (args.Length > 0)
            {
                addr = args[0];

                if (args.Length > 1)
                {
                    DisplayDetails = true;
                }
            }

            var client = new ArinClient();

            int thrown = 0;

            if (string.IsNullOrWhiteSpace(addr))
            {
                Console.WriteLine("Usage: ArinConsoleClient.exe ip-addr|path-to-file.txt");
            }
            else if (File.Exists(addr))
            {
                var file = File.ReadAllLines(addr);

                for (var i = 0; i < file.Length; i++)
                {
                    var ip = file[i];

                    Console.WriteLine($"# {i} = {ip}");

                    try
                    {
                        await Query(ip, client);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);

                        if (thrown++ > 10)
                        {
                            throw;
                        }
                    }
                }
            }
            else
            {
                await Query(addr, client);
            }
        }

        private static async Task Query(string addr, ArinClient client)
        {
            var response = await client.Query(IPAddress.Parse(addr));

            if (response is ErrorResponse error)
            {
                Console.WriteLine($"{error.ErrorCode}: {error.Title}");
                return;
            }

            if (response is IpResponse ip)
            {
                DisplayEntities(ip.Entities, "");

                Console.WriteLine();

                if (DisplayDetails)
                {
                    List<(string name, object value)> props = Explode(ip);

                    foreach (var (name, value) in props)
                    {
                        Console.WriteLine($"{name}: {value}");
                    }

                    Console.WriteLine();
                }
            }
        }

        private static void DisplayEntities(ICollection<Entity> entities, string prefix)
        {
            foreach (var entity in entities)
            {
                var line = $"{prefix}{entity.Handle}";

                if (entity.VCard != null && entity.VCard.TryGetValue("fn", out ContactCardProperty prop))
                {
                    Console.WriteLine($"{line}: {string.Join(" ", prop.Value)}");
                }
                else
                {
                    Console.WriteLine(line);
                }

                if (entity.VCard != null && entity.VCard.TryGetValue("email", out ContactCardProperty emailProp))
                {
                    Console.WriteLine($"{line}: {string.Join(" ", emailProp.Value)}");
                }

                DisplayEntities(entity.Entities, $"{prefix}   ");
            }
        }

        private static List<(string name, object value)> Explode(object thing, string prefix = "")
        {
            var props = new List<(string name, object value)>();

            var objectProps = thing.GetType().GetProperties().Where(p => p.PropertyType != typeof(JsonElement));

            foreach (var p in objectProps)
            {
                var type = p.PropertyType;
                var name = p.Name;

                var value = p.GetValue(thing);

                if (type.IsValueType || type.IsPrimitive || type == typeof(string))
                {
                    props.Add(($"{prefix}{name}", value));
                }
                else if (value is IEnumerable list)
                {
                    props.Add(($"{prefix}{name}", ""));

                    foreach (var item in list)
                    {
                        var elementType = item.GetType();

                        if (elementType.IsValueType || elementType.IsPrimitive || elementType == typeof(string))
                        {
                            props.Add(($"{prefix}", item));
                        }
                        else
                        {
                            var childList = Explode(item, $"{prefix}---");

                            props.AddRange(childList);
                        }
                    }
                }
            }

            return props;
        }
    }
}
