using System.Drawing;

namespace Task7.Data
{
    public class ColorGenerator
    {
        Random random;
        public ColorGenerator()
        {
            random = new Random();
        }

        public string GenerateColor()
        {
            return String.Format("#{0:X6}", random.Next(0x1000000));
        }
    }
}
