using System;

namespace WebMConverter
{
    public class LevelsFilter
    {
        public readonly LevelsConversion Type;

        public LevelsFilter(LevelsConversion Type)
        {
            this.Type = Type;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case LevelsConversion.TVtoPC:
                    return "ColorYUV(levels=\"TV->PC\")";
                case LevelsConversion.PCtoTV:
                    return "ColorYUV(levels=\"PC->TV\")";
                default:
                    throw new NotImplementedException();
            }
            
        }
    }

    public enum LevelsConversion
    {
        TVtoPC, // expanding to the full PC range
        PCtoTV  // fixing FRAPS videos
    }
}