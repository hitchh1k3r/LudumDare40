// Color names are from the CSS3 specification, Section 4.3 Extended Color Keywords
// http://www.w3.org/TR/css3-color/#svg-color

using UnityEngine;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public static class Colors
  {

    // START Definitions

    public static readonly Color aliceBlue = new Color32(240, 248, 255, 255);
    public static readonly Color antiqueWhite = new Color32(250, 235, 215, 255);
    public static readonly Color aqua = new Color32(0, 255, 255, 255);
    public static readonly Color aquamarine = new Color32(127, 255, 212, 255);
    public static readonly Color azure = new Color32(240, 255, 255, 255);
    public static readonly Color beige = new Color32(245, 245, 220, 255);
    public static readonly Color bisque = new Color32(255, 228, 196, 255);
    public static readonly Color black = new Color32(0, 0, 0, 255);
    public static readonly Color blanchedAlmond = new Color32(255, 235, 205, 255);
    public static readonly Color blue = new Color32(0, 0, 255, 255);
    public static readonly Color blueViolet = new Color32(138, 43, 226, 255);
    public static readonly Color brown = new Color32(165, 42, 42, 255);
    public static readonly Color burlywood = new Color32(222, 184, 135, 255);
    public static readonly Color cadetBlue = new Color32(95, 158, 160, 255);
    public static readonly Color chartreuse = new Color32(127, 255, 0, 255);
    public static readonly Color chocolate = new Color32(210, 105, 30, 255);
    public static readonly Color coral = new Color32(255, 127, 80, 255);
    public static readonly Color cornflowerBlue = new Color32(100, 149, 237, 255);
    public static readonly Color cornsilk = new Color32(255, 248, 220, 255);
    public static readonly Color crimson = new Color32(220, 20, 60, 255);
    public static readonly Color cyan = new Color32(0, 255, 255, 255);
    public static readonly Color darkBlue = new Color32(0, 0, 139, 255);
    public static readonly Color darkCyan = new Color32(0, 139, 139, 255);
    public static readonly Color darkGoldenrod = new Color32(184, 134, 11, 255);
    public static readonly Color darkGray = new Color32(169, 169, 169, 255);
    public static readonly Color darkGreen = new Color32(0, 100, 0, 255);
    public static readonly Color darkKhaki = new Color32(189, 183, 107, 255);
    public static readonly Color darkMagenta = new Color32(139, 0, 139, 255);
    public static readonly Color darkOliveGreen = new Color32(85, 107, 47, 255);
    public static readonly Color darkOrange = new Color32(255, 140, 0, 255);
    public static readonly Color darkOrchid = new Color32(153, 50, 204, 255);
    public static readonly Color darkRed = new Color32(139, 0, 0, 255);
    public static readonly Color darkSalmon = new Color32(233, 150, 122, 255);
    public static readonly Color darkSeaGreen = new Color32(143, 188, 143, 255);
    public static readonly Color darkSlateBlue = new Color32(72, 61, 139, 255);
    public static readonly Color darkSlateGray = new Color32(47, 79, 79, 255);
    public static readonly Color darkTurquoise = new Color32(0, 206, 209, 255);
    public static readonly Color darkViolet = new Color32(148, 0, 211, 255);
    public static readonly Color deepPink = new Color32(255, 20, 147, 255);
    public static readonly Color deepSkyBlue = new Color32(0, 191, 255, 255);
    public static readonly Color dimGray = new Color32(105, 105, 105, 255);
    public static readonly Color dodgerBlue = new Color32(30, 144, 255, 255);
    public static readonly Color fireBrick = new Color32(178, 34, 34, 255);
    public static readonly Color floralWhite = new Color32(255, 250, 240, 255);
    public static readonly Color forestGreen = new Color32(34, 139, 34, 255);
    public static readonly Color fuchsia = new Color32(255, 0, 255, 255);
    public static readonly Color gainsboro = new Color32(220, 220, 220, 255);
    public static readonly Color ghostWhite = new Color32(248, 248, 255, 255);
    public static readonly Color gold = new Color32(255, 215, 0, 255);
    public static readonly Color goldenrod = new Color32(218, 165, 32, 255);
    public static readonly Color gray = new Color32(128, 128, 128, 255);
    public static readonly Color green = new Color32(0, 128, 0, 255);
    public static readonly Color greenYellow = new Color32(173, 255, 47, 255);
    public static readonly Color honeydew = new Color32(240, 255, 240, 255);
    public static readonly Color hotPink = new Color32(255, 105, 180, 255);
    public static readonly Color indianRed = new Color32(205, 92, 92, 255);
    public static readonly Color indigo = new Color32(75, 0, 130, 255);
    public static readonly Color ivory = new Color32(255, 255, 240, 255);
    public static readonly Color khaki = new Color32(240, 230, 140, 255);
    public static readonly Color lavender = new Color32(230, 230, 250, 255);
    public static readonly Color lavenderblush = new Color32(255, 240, 245, 255);
    public static readonly Color lawnGreen = new Color32(124, 252, 0, 255);
    public static readonly Color lemonChiffon = new Color32(255, 250, 205, 255);
    public static readonly Color lightBlue = new Color32(173, 216, 230, 255);
    public static readonly Color lightCoral = new Color32(240, 128, 128, 255);
    public static readonly Color lightCyan = new Color32(224, 255, 255, 255);
    public static readonly Color lightGoldenodYellow = new Color32(250, 250, 210, 255);
    public static readonly Color lightGray = new Color32(211, 211, 211, 255);
    public static readonly Color lightGreen = new Color32(144, 238, 144, 255);
    public static readonly Color lightPink = new Color32(255, 182, 193, 255);
    public static readonly Color lightSalmon = new Color32(255, 160, 122, 255);
    public static readonly Color lightSeaGreen = new Color32(32, 178, 170, 255);
    public static readonly Color lightSkyBlue = new Color32(135, 206, 250, 255);
    public static readonly Color lightSlateGray = new Color32(119, 136, 153, 255);
    public static readonly Color lightSteelBlue = new Color32(176, 196, 222, 255);
    public static readonly Color lightYellow = new Color32(255, 255, 224, 255);
    public static readonly Color lime = new Color32(0, 255, 0, 255);
    public static readonly Color limeGreen = new Color32(50, 205, 50, 255);
    public static readonly Color linen = new Color32(250, 240, 230, 255);
    public static readonly Color magenta = new Color32(255, 0, 255, 255);
    public static readonly Color maroon = new Color32(128, 0, 0, 255);
    public static readonly Color mediumAquamarine = new Color32(102, 205, 170, 255);
    public static readonly Color mediumBlue = new Color32(0, 0, 205, 255);
    public static readonly Color mediumOrchid = new Color32(186, 85, 211, 255);
    public static readonly Color mediumPurple = new Color32(147, 112, 219, 255);
    public static readonly Color mediumSeaGreen = new Color32(60, 179, 113, 255);
    public static readonly Color mediumSlateBlue = new Color32(123, 104, 238, 255);
    public static readonly Color mediumSpringGreen = new Color32(0, 250, 154, 255);
    public static readonly Color mediumTurquoise = new Color32(72, 209, 204, 255);
    public static readonly Color mediumVioletRed = new Color32(199, 21, 133, 255);
    public static readonly Color midnightBlue = new Color32(25, 25, 112, 255);
    public static readonly Color mintcream = new Color32(245, 255, 250, 255);
    public static readonly Color mistyRose = new Color32(255, 228, 225, 255);
    public static readonly Color moccasin = new Color32(255, 228, 181, 255);
    public static readonly Color navajoWhite = new Color32(255, 222, 173, 255);
    public static readonly Color navy = new Color32(0, 0, 128, 255);
    public static readonly Color oldLace = new Color32(253, 245, 230, 255);
    public static readonly Color olive = new Color32(128, 128, 0, 255);
    public static readonly Color olivedrab = new Color32(107, 142, 35, 255);
    public static readonly Color orange = new Color32(255, 165, 0, 255);
    public static readonly Color orangeRed = new Color32(255, 69, 0, 255);
    public static readonly Color orchid = new Color32(218, 112, 214, 255);
    public static readonly Color paleGoldenrod = new Color32(238, 232, 170, 255);
    public static readonly Color paleGreen = new Color32(152, 251, 152, 255);
    public static readonly Color paleTurquoise = new Color32(175, 238, 238, 255);
    public static readonly Color paleVioletRed = new Color32(219, 112, 147, 255);
    public static readonly Color papayaWhip = new Color32(255, 239, 213, 255);
    public static readonly Color peachPuff = new Color32(255, 218, 185, 255);
    public static readonly Color peru = new Color32(205, 133, 63, 255);
    public static readonly Color pink = new Color32(255, 192, 203, 255);
    public static readonly Color plum = new Color32(221, 160, 221, 255);
    public static readonly Color powderBlue  = new Color32(176, 224, 230, 255);
    public static readonly Color purple = new Color32(128, 0, 128, 255);
    public static readonly Color red = new Color32(255, 0, 0, 255);
    public static readonly Color rosyBrown = new Color32(188, 143, 143, 255);
    public static readonly Color royalBlue = new Color32(65, 105, 225, 255);
    public static readonly Color saddleBrown = new Color32(139, 69, 19, 255);
    public static readonly Color salmon = new Color32(250, 128, 114, 255);
    public static readonly Color sandyBrown = new Color32(244, 164, 96, 255);
    public static readonly Color seaGreen = new Color32(46, 139, 87, 255);
    public static readonly Color seashell = new Color32(255, 245, 238, 255);
    public static readonly Color sienna = new Color32(160, 82, 45, 255);
    public static readonly Color silver = new Color32(192, 192, 192, 255);
    public static readonly Color skyBlue = new Color32(135, 206, 235, 255);
    public static readonly Color slateBlue = new Color32(106, 90, 205, 255);
    public static readonly Color slateGray = new Color32(112, 128, 144, 255);
    public static readonly Color snow = new Color32(255, 250, 250, 255);
    public static readonly Color springGreen = new Color32(0, 255, 127, 255);
    public static readonly Color steelBlue = new Color32(70, 130, 180, 255);
    public static readonly Color tan = new Color32(210, 180, 140, 255);
    public static readonly Color teal = new Color32(0, 128, 128, 255);
    public static readonly Color thistle = new Color32(216, 191, 216, 255);
    public static readonly Color tomato = new Color32(255, 99, 71, 255);
    public static readonly Color turquoise = new Color32(64, 224, 208, 255);
    public static readonly Color violet = new Color32(238, 130, 238, 255);
    public static readonly Color wheat = new Color32(245, 222, 179, 255);
    public static readonly Color white = new Color32(255, 255, 255, 255);
    public static readonly Color whiteSmoke = new Color32(245, 245, 245, 255);
    public static readonly Color yellow = new Color32(255, 255, 0, 255);
    public static readonly Color yellowGreen = new Color32(154, 205, 50, 255);

    // END Definitions
    // START Public Utilities

    public static Color FromEnum(ColorEnum color)
    {
      switch(color)
      {
        case ColorEnum.ALICE_BLUE: return aliceBlue;
        case ColorEnum.ANTIQUE_WHITE: return antiqueWhite;
        case ColorEnum.AQUA: return aqua;
        case ColorEnum.AQUAMARINE: return aquamarine;
        case ColorEnum.AZURE: return azure;
        case ColorEnum.BEIGE: return beige;
        case ColorEnum.BISQUE: return bisque;
        case ColorEnum.BLACK: return black;
        case ColorEnum.BLANCHED_ALMOND: return blanchedAlmond;
        case ColorEnum.BLUE: return blue;
        case ColorEnum.BLUE_VIOLET: return blueViolet;
        case ColorEnum.BROWN: return brown;
        case ColorEnum.BURLYWOOD: return burlywood;
        case ColorEnum.CADET_BLUE: return cadetBlue;
        case ColorEnum.CHARTREUSE: return chartreuse;
        case ColorEnum.CHOCOLATE: return chocolate;
        case ColorEnum.CORAL: return coral;
        case ColorEnum.CORNFLOWER_BLUE: return cornflowerBlue;
        case ColorEnum.CORNSILK: return cornsilk;
        case ColorEnum.CRIMSON: return crimson;
        case ColorEnum.CYAN: return cyan;
        case ColorEnum.DARK_BLUE: return darkBlue;
        case ColorEnum.DARK_CYAN: return darkCyan;
        case ColorEnum.DARK_GOLDENROD: return darkGoldenrod;
        case ColorEnum.DARK_GRAY: return darkGray;
        case ColorEnum.DARK_GREEN: return darkGreen;
        case ColorEnum.DARK_KHAKI: return darkKhaki;
        case ColorEnum.DARK_MAGENTA: return darkMagenta;
        case ColorEnum.DARK_OLIVE_GREEN: return darkOliveGreen;
        case ColorEnum.DARK_ORANGE: return darkOrange;
        case ColorEnum.DARK_ORCHID: return darkOrchid;
        case ColorEnum.DARK_RED: return darkRed;
        case ColorEnum.DARK_SALMON: return darkSalmon;
        case ColorEnum.DARK_SEA_GREEN: return darkSeaGreen;
        case ColorEnum.DARK_SLATE_BLUE: return darkSlateBlue;
        case ColorEnum.DARK_SLATE_GRAY: return darkSlateGray;
        case ColorEnum.DARK_TURQUOISE: return darkTurquoise;
        case ColorEnum.DARK_VIOLET: return darkViolet;
        case ColorEnum.DEEP_PINK: return deepPink;
        case ColorEnum.DEEP_SKY_BLUE: return deepSkyBlue;
        case ColorEnum.DIM_GRAY: return dimGray;
        case ColorEnum.DODGER_BLUE: return dodgerBlue;
        case ColorEnum.FIRE_BRICK: return fireBrick;
        case ColorEnum.FLORAL_WHITE: return floralWhite;
        case ColorEnum.FOREST_GREEN: return forestGreen;
        case ColorEnum.FUCHSIA: return fuchsia;
        case ColorEnum.GAINSBORO: return gainsboro;
        case ColorEnum.GHOST_WHITE: return ghostWhite;
        case ColorEnum.GOLD: return gold;
        case ColorEnum.GOLDENROD: return goldenrod;
        case ColorEnum.GRAY: return gray;
        case ColorEnum.GREEN: return green;
        case ColorEnum.GREEN_YELLOW: return greenYellow;
        case ColorEnum.HONEYDEW: return honeydew;
        case ColorEnum.HOT_PINK: return hotPink;
        case ColorEnum.INDIAN_RED: return indianRed;
        case ColorEnum.INDIGO: return indigo;
        case ColorEnum.IVORY: return ivory;
        case ColorEnum.KHAKI: return khaki;
        case ColorEnum.LAVENDER: return lavender;
        case ColorEnum.LAVENDERBLUSH: return lavenderblush;
        case ColorEnum.LAWN_GREEN: return lawnGreen;
        case ColorEnum.LEMON_CHIFFON: return lemonChiffon;
        case ColorEnum.LIGHT_BLUE: return lightBlue;
        case ColorEnum.LIGHT_CORAL: return lightCoral;
        case ColorEnum.LIGHT_CYAN: return lightCyan;
        case ColorEnum.LIGHT_GOLDENOD_YELLOW: return lightGoldenodYellow;
        case ColorEnum.LIGHT_GRAY: return lightGray;
        case ColorEnum.LIGHT_GREEN: return lightGreen;
        case ColorEnum.LIGHT_PINK: return lightPink;
        case ColorEnum.LIGHT_SALMON: return lightSalmon;
        case ColorEnum.LIGHT_SEA_GREEN: return lightSeaGreen;
        case ColorEnum.LIGHT_SKY_BLUE: return lightSkyBlue;
        case ColorEnum.LIGHT_SLATE_GRAY: return lightSlateGray;
        case ColorEnum.LIGHT_STEEL_BLUE: return lightSteelBlue;
        case ColorEnum.LIGHT_YELLOW: return lightYellow;
        case ColorEnum.LIME: return lime;
        case ColorEnum.LIME_GREEN: return limeGreen;
        case ColorEnum.LINEN: return linen;
        case ColorEnum.MAGENTA: return magenta;
        case ColorEnum.MAROON: return maroon;
        case ColorEnum.MEDIUM_AQUAMARINE: return mediumAquamarine;
        case ColorEnum.MEDIUM_BLUE: return mediumBlue;
        case ColorEnum.MEDIUM_ORCHID: return mediumOrchid;
        case ColorEnum.MEDIUM_PURPLE: return mediumPurple;
        case ColorEnum.MEDIUM_SEA_GREEN: return mediumSeaGreen;
        case ColorEnum.MEDIUM_SLATE_BLUE: return mediumSlateBlue;
        case ColorEnum.MEDIUM_SPRING_GREEN: return mediumSpringGreen;
        case ColorEnum.MEDIUM_TURQUOISE: return mediumTurquoise;
        case ColorEnum.MEDIUM_VIOLET_RED: return mediumVioletRed;
        case ColorEnum.MIDNIGHT_BLUE: return midnightBlue;
        case ColorEnum.MINTCREAM: return mintcream;
        case ColorEnum.MISTY_ROSE: return mistyRose;
        case ColorEnum.MOCCASIN: return moccasin;
        case ColorEnum.NAVAJO_WHITE: return navajoWhite;
        case ColorEnum.NAVY: return navy;
        case ColorEnum.OLD_LACE: return oldLace;
        case ColorEnum.OLIVE: return olive;
        case ColorEnum.OLIVEDRAB: return olivedrab;
        case ColorEnum.ORANGE: return orange;
        case ColorEnum.ORANGE_RED: return orangeRed;
        case ColorEnum.ORCHID: return orchid;
        case ColorEnum.PALE_GOLDENROD: return paleGoldenrod;
        case ColorEnum.PALE_GREEN: return paleGreen;
        case ColorEnum.PALE_TURQUOISE: return paleTurquoise;
        case ColorEnum.PALE_VIOLET_RED: return paleVioletRed;
        case ColorEnum.PAPAYA_WHIP: return papayaWhip;
        case ColorEnum.PEACH_PUFF: return peachPuff;
        case ColorEnum.PERU: return peru;
        case ColorEnum.PINK: return pink;
        case ColorEnum.PLUM: return plum;
        case ColorEnum.POWDER_BLUE: return powderBlue ;
        case ColorEnum.PURPLE: return purple;
        case ColorEnum.RED: return red;
        case ColorEnum.ROSY_BROWN: return rosyBrown;
        case ColorEnum.ROYAL_BLUE: return royalBlue;
        case ColorEnum.SADDLE_BROWN: return saddleBrown;
        case ColorEnum.SALMON: return salmon;
        case ColorEnum.SANDY_BROWN: return sandyBrown;
        case ColorEnum.SEA_GREEN: return seaGreen;
        case ColorEnum.SEASHELL: return seashell;
        case ColorEnum.SIENNA: return sienna;
        case ColorEnum.SILVER: return silver;
        case ColorEnum.SKY_BLUE: return skyBlue;
        case ColorEnum.SLATE_BLUE: return slateBlue;
        case ColorEnum.SLATE_GRAY: return slateGray;
        case ColorEnum.SNOW: return snow;
        case ColorEnum.SPRING_GREEN: return springGreen;
        case ColorEnum.STEEL_BLUE: return steelBlue;
        case ColorEnum.TAN: return tan;
        case ColorEnum.TEAL: return teal;
        case ColorEnum.THISTLE: return thistle;
        case ColorEnum.TOMATO: return tomato;
        case ColorEnum.TURQUOISE: return turquoise;
        case ColorEnum.VIOLET: return violet;
        case ColorEnum.WHEAT: return wheat;
        case ColorEnum.WHITE: return white;
        case ColorEnum.WHITE_SMOKE: return whiteSmoke;
        case ColorEnum.YELLOW: return yellow;
        case ColorEnum.YELLOW_GREEN: return yellowGreen;
      }
      return Color.white;
    }

    // END Public Utilities

  }

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public enum ColorEnum
  {
    ALICE_BLUE, ANTIQUE_WHITE, AQUA, AQUAMARINE, AZURE, BEIGE, BISQUE, BLACK, BLANCHED_ALMOND, BLUE,
    BLUE_VIOLET, BROWN, BURLYWOOD, CADET_BLUE, CHARTREUSE, CHOCOLATE, CORAL, CORNFLOWER_BLUE,
    CORNSILK, CRIMSON, CYAN, DARK_BLUE, DARK_CYAN, DARK_GOLDENROD, DARK_GRAY, DARK_GREEN,
    DARK_KHAKI, DARK_MAGENTA, DARK_OLIVE_GREEN, DARK_ORANGE, DARK_ORCHID, DARK_RED, DARK_SALMON,
    DARK_SEA_GREEN, DARK_SLATE_BLUE, DARK_SLATE_GRAY, DARK_TURQUOISE, DARK_VIOLET, DEEP_PINK,
    DEEP_SKY_BLUE, DIM_GRAY, DODGER_BLUE, FIRE_BRICK, FLORAL_WHITE, FOREST_GREEN, FUCHSIA,
    GAINSBORO, GHOST_WHITE, GOLD, GOLDENROD, GRAY, GREEN, GREEN_YELLOW, HONEYDEW, HOT_PINK,
    INDIAN_RED, INDIGO, IVORY, KHAKI, LAVENDER, LAVENDERBLUSH, LAWN_GREEN, LEMON_CHIFFON,
    LIGHT_BLUE, LIGHT_CORAL, LIGHT_CYAN, LIGHT_GOLDENOD_YELLOW, LIGHT_GRAY, LIGHT_GREEN, LIGHT_PINK,
    LIGHT_SALMON, LIGHT_SEA_GREEN, LIGHT_SKY_BLUE, LIGHT_SLATE_GRAY, LIGHT_STEEL_BLUE, LIGHT_YELLOW,
    LIME, LIME_GREEN, LINEN, MAGENTA, MAROON, MEDIUM_AQUAMARINE, MEDIUM_BLUE, MEDIUM_ORCHID,
    MEDIUM_PURPLE, MEDIUM_SEA_GREEN, MEDIUM_SLATE_BLUE, MEDIUM_SPRING_GREEN, MEDIUM_TURQUOISE,
    MEDIUM_VIOLET_RED, MIDNIGHT_BLUE, MINTCREAM, MISTY_ROSE, MOCCASIN, NAVAJO_WHITE, NAVY, OLD_LACE,
    OLIVE, OLIVEDRAB, ORANGE, ORANGE_RED, ORCHID, PALE_GOLDENROD, PALE_GREEN, PALE_TURQUOISE,
    PALE_VIOLET_RED, PAPAYA_WHIP, PEACH_PUFF, PERU, PINK, PLUM, POWDER_BLUE, PURPLE, RED,
    ROSY_BROWN, ROYAL_BLUE, SADDLE_BROWN, SALMON, SANDY_BROWN, SEA_GREEN, SEASHELL, SIENNA, SILVER,
    SKY_BLUE, SLATE_BLUE, SLATE_GRAY, SNOW, SPRING_GREEN, STEEL_BLUE, TAN, TEAL, THISTLE, TOMATO,
    TURQUOISE, VIOLET, WHEAT, WHITE, WHITE_SMOKE, YELLOW, YELLOW_GREEN
  }

}
