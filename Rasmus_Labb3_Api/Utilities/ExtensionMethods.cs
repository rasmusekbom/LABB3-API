﻿using Rasmus_labb3_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rasmus_labb3_API.Utilities
{
    public static class ExtensionMethods
    {
        // Method to get and return a specified person by Name.
        public static Person PersonByName(this IEnumerable<Person> persons, string name)
        {
            return persons.FirstOrDefault(person => person.Name.ToLower().Contains(name.ToLower()));
        }

        // Method to get and return an Interest based on specified Name.
        public static Interest InterestByName(this IEnumerable<Interest> interests, string name)
        {
            return interests.FirstOrDefault(interest => interest.Title.ToLower().Contains(name.ToLower()));
        }
    }
}
