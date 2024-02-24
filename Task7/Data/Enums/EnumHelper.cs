using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Task7.Data.Enums
{
    public enum GameType
    {
        [Display(Name = "Tic Tac Toe")]
        TicTacToe,
        [Display(Name = "Five in row")]
        BiggerTicTacToe,
        [Display(Name = "Find pair")]
        FindPair

    }
    public static class EnumHelper
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue
                      .GetType()
                      .GetMember(enumValue.ToString())
                      .First()?
                      .GetCustomAttribute<DisplayAttribute>()?
                      .Name;
        }
    }
}
