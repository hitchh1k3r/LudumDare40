using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public static class Tweening
  {

    // Easy Tweens:

    public static IEnumerator EasyUIShow(CanvasGroup canvasGroup, float length = 0.75f,
          Vector2? positionStart = null, Vector2? positionEnd = null,
          Easing.EaseMethod easingFade = null, Easing.EaseMethod easingMove = null,
          Action callbackEnding = null)
    {
      // EasyUIShow is a quick method for showing a CanvasGroup UI Element. It trys to automatically
      //   guess the ideal settings for a fade in, optionally taking a start and/or end position too
      //   for a sliding fade in.
      easingFade = easingFade ?? Easing.EASE_QUAD_IN;
      if(positionStart.HasValue || positionEnd.HasValue)
      {
        easingMove = easingMove ?? Easing.EASE_QUAD_OUT;
        return UIFadeMove(canvasGroup, (RectTransform) canvasGroup.transform, 0, 1, positionStart,
              positionEnd, length, easingFade, easingMove, callbackEnding);
      }
      else
      {
        return UIFade(canvasGroup, 0, 1, length, easingFade, callbackEnding);
      }
    }

    public static IEnumerator EasyUIHide(CanvasGroup canvasGroup, float length = 0.75f,
          Vector2? positionStart = null, Vector2? positionEnd = null,
          Easing.EaseMethod easingFade = null, Easing.EaseMethod easingMove = null,
          Action callbackEnding = null)
    {
      // EasyUIHide is a quick method for hidding a CanvasGroup UI Element. It trys to automatically
      //   guess the ideal settings for a fade out, optionally taking a start and/or end position
      //   too for a sliding fade out.
      easingFade = easingFade ?? Easing.EASE_QUAD_IN;
      if(positionStart.HasValue || positionEnd.HasValue)
      {
        easingMove = easingMove ?? Easing.EASE_QUAD_IN;
        return UIFadeMove(canvasGroup, (RectTransform) canvasGroup.transform, null, 0,
              positionStart, positionEnd, length, easingFade, easingMove, callbackEnding);
      }
      else
      {
        return UIFade(canvasGroup, null, 0, length, easingFade, callbackEnding);
      }
    }

    public static IEnumerator EasyPopIn(Transform transform, float length = 1.5f,
          Vector3? target = null, Easing.EaseMethod easing = null,
          Action callbackEnding = null)
    {
      // EasyPopIn is a quick method for scaling a 3D object (by Transform) out of nothing.
      Vector3 endScale = target ?? Vector3.one;
      easing = easing ?? Easing.EASE_ELASTIC_OUT;
      return TransScale(transform, Vector3.zero, endScale, length, easing, callbackEnding);
    }

    public static IEnumerator EasyPopOut(Transform transform, float length = 1.5f,
          Easing.EaseMethod easing = null, Action callbackEnding = null)
    {
      // EasyPopOut is a quick method for scaling a 3D object (by Transform) down to nothing.
      easing = easing ?? Easing.EASE_QUAD_IN;
      return TransScale(transform, null, Vector3.zero, length, easing, callbackEnding);
    }


    // Transform Tweens:

    public static IEnumerator TransTranslate(Transform transform, Vector3? positionStart,
          Vector3? positionEnd, float length, Easing.EaseMethod easing = null,
          bool global = true, Action callbackEnding = null)
    {
      // TransTranslate moves a Transform in either local or global space. If the starting or ending
      //   positions are NULL it will use the current position in it's place.
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;
      Vector3 start = positionStart ?? (global ? transform.position : transform.localPosition);
      Vector3 end = positionEnd ?? (global ? transform.position : transform.localPosition);

      while(time < length)
      {
        float t = easing(time / length);

        if(global)
        {
          transform.position = (1-t) * start + t * end;
        }
        else
        {
          transform.localPosition = (1-t) * start + t * end;
        }

        yield return null;
        time += Time.deltaTime;
      }

      if(global)
      {
        transform.position = end;
      }
      else
      {
        transform.localPosition = end;
      }
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }

    public static IEnumerator TransRotate(Transform transform, Quaternion? startRot,
          Quaternion? endRot, float length, Easing.EaseMethod easing = null, bool global = false,
          Action callbackEnding = null)
    {
      // TransRotate rotates a Transform in either local or global space. If the starting or ending
      //   rotations are NULL it will use the current rotation in it's place.
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;
      Quaternion start = startRot ?? (global ? transform.rotation : transform.localRotation);
      Quaternion end = endRot ?? (global ? transform.rotation : transform.localRotation);

      while(time < length)
      {
        float t = easing(time / length);

        if(global)
        {
          transform.rotation = Quaternion.SlerpUnclamped(start, end, t);
        }
        else
        {
          transform.localRotation = Quaternion.SlerpUnclamped(start, end, t);
        }

        yield return null;
        time += Time.deltaTime;
      }

      if(global)
      {
        transform.rotation = end;
      }
      else
      {
        transform.localRotation = end;
      }
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }

    public static IEnumerator TransScale(Transform transform, Vector3? startScale, Vector3? endScale,
          float length, Easing.EaseMethod easing = null, Action callbackEnding = null)
    {
      // TransScale scales a Transform in ONLY local space. If the starting or ending scale are NULL
      //   it will use the current scale in it's place.
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;
      Vector3 start = startScale ?? transform.localScale;
      Vector3 end = endScale ?? transform.localScale;

      while(time < length)
      {
        float t = easing(time / length);

        transform.localScale = (1-t) * start + t * end;

        yield return null;
        time += Time.deltaTime;
      }

      transform.localScale = end;
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }

    public static IEnumerator Transform(Transform transform, Vector3? positionStart,
          Quaternion? startRot, Vector3? startScale, Vector3? positionEnd, Quaternion? endRot,
          Vector3? endScale, float length, Easing.EaseMethod easing = null, bool global = true,
          Action callbackEnding = null)
    {
      // Transform translate, rotate, and scales (LOCAL SPACE ONLY) a Transform in either local or
      //   global space. Any starting or ending values that are NULL will use the current value in
      //   it's place.
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;
      Vector3 startP, endP;
      Quaternion startR, endR;
      Vector3 startS = positionStart ?? transform.localScale;
      Vector3 endS = endScale ?? transform.localScale;
      if(global)
      {
        startP = positionStart ?? transform.position;
        endP = positionEnd ?? transform.position;
        startR = startRot ?? transform.rotation;
        endR = endRot ?? transform.rotation;
      }
      else
      {
        startP = positionStart ?? transform.localPosition;
        endP = positionEnd ?? transform.localPosition;
        startR = startRot ?? transform.localRotation;
        endR = endRot ?? transform.localRotation;
      }

      while(time < length)
      {
        float t = easing(time / length);

        if(global)
        {
          transform.position = (1-t) * startP + t * endP;
          transform.rotation = Quaternion.SlerpUnclamped(startR, endR, t);
        }
        else
        {
          transform.localPosition = (1-t) * startP + t * endP;
          transform.localRotation = Quaternion.SlerpUnclamped(startR, endR, t);
        }
        transform.localScale = (1-t) * startS + t * endS;

        yield return null;
        time += Time.deltaTime;
      }

      if(global)
      {
        transform.position = endP;
        transform.rotation = endR;
      }
      else
      {
        transform.localPosition = endP;
        transform.localRotation = endR;
      }
      transform.localScale = endS;
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }

    public static IEnumerator Transform(Transform transform, Transform startTrans,
          Transform endTrans, float length, Easing.EaseMethod easing = null,
          bool trackStart = false, bool trackEnd = false, bool global = true,
          Action callbackEnding = null)
    {
      // Transform translate, rotate, and scales (LOCAL SPACE ONLY) a Transform in either local or
      //   global space. This version uses a start and end transform rather then set of values. If
      //   the start or end are null they will use the current values.
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;
      Vector3 positionStart, positionEnd;
      Quaternion startRot, endRot;
      Vector3 startScale = startTrans != null ? startTrans.localScale : transform.localScale;
      Vector3 endScale = endTrans != null ? endTrans.localScale : transform.localScale;
      if(global)
      {
        positionStart = (startTrans ?? transform).position;
        positionEnd = (endTrans ?? transform).position;
        startRot = (startTrans ?? transform).rotation;
        endRot = (endTrans ?? transform).rotation;
      }
      else
      {
        positionStart = (startTrans ?? transform).localPosition;
        positionEnd = (endTrans ?? transform).localPosition;
        startRot = (startTrans ?? transform).localRotation;
        endRot = (endTrans ?? transform).localRotation;
      }

      while(time < length)
      {
        float t = easing(time / length);

        if(trackStart)
        {
          if(startTrans != null)
          {
            positionStart = global ? startTrans.position : startTrans.localPosition;
            startRot = global ? startTrans.rotation : startTrans.localRotation;
            startScale = startTrans.localScale;
          }
        }
        if(trackEnd)
        {
          if(endTrans != null)
          {
            positionEnd = global ? endTrans.position : endTrans.localPosition;
            endRot = global ? endTrans.rotation : endTrans.localRotation;
            endScale = endTrans.localScale;
          }
        }

        if(global)
        {
          transform.position = (1-t) * positionStart + t * positionEnd;
          transform.rotation = Quaternion.SlerpUnclamped(startRot, endRot, t);
        }
        else
        {
          transform.localPosition = (1-t) * positionStart + t * positionEnd;
          transform.localRotation = Quaternion.SlerpUnclamped(startRot, endRot, t);
        }
        transform.localScale = (1-t) * startScale + t * endScale;

        yield return null;
        time += Time.deltaTime;
      }

      if(global)
      {
        transform.position = positionEnd;
        transform.rotation = endRot;
      }
      else
      {
        transform.localPosition = positionEnd;
        transform.localRotation = endRot;
      }
      transform.localScale = endScale;
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }


    // UI Tweens:

    // NOTE (hitch) 11-28-17 Changes to any UI element will cause the parent Canvas to redraw all
    //   children. This means a tween (changing every frame) could cause lag if the Canvas has too
    //   many elements. Multiple Canvases should be considered to limit what has to be redrawn.

    public static IEnumerator UIColor(Graphic element, Color? startColor, Color? endColor,
          float length, Easing.EaseMethod easing = null, Action callbackEnding = null)
    {
      // UIColor changes the color of any UI Graphic element. If the start or end color are NULL
      //   the transistion will use the current color.
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;
      Color start = startColor ?? element.color;
      Color end = endColor ?? element.color;

      while(time < length)
      {
        float t = easing(time / length);

        element.color = (1-t) * start + t * end;

        yield return null;
        time += Time.deltaTime;
      }

      element.color = end;
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }

    public static IEnumerator UIFade(CanvasGroup canvasGroup, float? startAlpha, float? endAlpha,
          float length, Easing.EaseMethod easing = null, Action callbackEnding = null)
    {
      // UIFade changes the opacity of a UI CanvasGroup element. If the start or end alpha are NULL
      //   the transistion will use the current alpha.
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;
      float start = startAlpha ?? canvasGroup.alpha;
      float end = endAlpha ?? canvasGroup.alpha;

      while(time < length)
      {
        float t = easing(time / length);

        canvasGroup.alpha = (1-t) * start + t * end;

        yield return null;
        time += Time.deltaTime;
      }

      canvasGroup.alpha = end;
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }

    public static IEnumerator UIMove(RectTransform rect, Vector2? positionStart = null,
          Vector2? positionEnd = null, float length = 0.75f, Easing.EaseMethod easing = null,
          Action callbackEnding = null)
    {
      // UIMove moves a RectTransform by anchroedPosition. If the start or end position are NULL
      //   the transistion will use the current position.
      Vector2 start = positionStart ?? rect.anchoredPosition;
      Vector2 end = positionEnd ?? rect.anchoredPosition;
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;

      while(time < length)
      {
        float t = easing(time / length);

        rect.anchoredPosition = (1-t) * start + t * end;

        yield return null;
        time += Time.deltaTime;
      }

      rect.anchoredPosition = end;
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }

    public static IEnumerator UIFadeMove(CanvasGroup canvasGroup, RectTransform rect, float? startAlpha,
          float? endAlpha, Vector2? positionStart, Vector2? positionEnd, float length = 0.75f,
          Easing.EaseMethod easingFade = null, Easing.EaseMethod easingMove = null,
          Action callbackEnding = null)
    {
      // UIFadeMove moves and changes the opacity of a UI CanvasGroup and RectTransform. If the
      //   start or end alpha or position are NULL they will use the current values.

      // JUSTIFICATION: These attributes are changed together offten for UI changes, having them in
      //   a single method means we only have to pay for one Coroutine, and not two... the trade off
      //   if that they both must finish at the same time.
      float startA = startAlpha ?? canvasGroup.alpha;
      float endA = endAlpha ?? canvasGroup.alpha;
      Vector2 startP = positionStart ?? rect.anchoredPosition;
      Vector2 endP = positionEnd ?? rect.anchoredPosition;
      easingFade = easingFade ?? Easing.EASE_LINEAR;
      easingMove = easingMove ?? Easing.EASE_LINEAR;
      float time = 0;

      while(time < length)
      {
        float t = time / length;
        float ft = easingFade(t);
        float mt = easingMove(t);

        canvasGroup.alpha = (1-ft) * startA + ft * endA;
        rect.anchoredPosition = (1-mt) * startP + mt * endP;

        yield return null;
        time += Time.deltaTime;
      }

      canvasGroup.alpha = endA;
      rect.anchoredPosition = endP;
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }


    // Custom Tween:

    public static IEnumerator Action(Action<float> setOutput, float length,
          Easing.EaseMethod easing = null, Action callbackEnding = null)
    {
      // Action will call a setOutput Action with the current tween ammount (0 to 1) each frame.

      // JUSTIFICATION: Generic seems too expensive, this should be (untested) a cheaper version. We
      //   don't have a type check/casting nightmare happening every frame, but we still have a
      //   callback. The callback is necissary (as long as we can't use pointers) to set variables
      //   that are not part of a well understood data structure (ex. Transforms).
      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;

      while(time < length)
      {
        float t = easing(time / length);

        setOutput(t);
        yield return null;
        time += Time.deltaTime;
      }

      setOutput(1);
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }


    // Generic Tween:

    public static IEnumerator Generic<T>(Action<T> setOutput, T start, T end, float length,
          Easing.EaseMethod easing = null, Action callbackEnding = null)
    {
      // Generic will call a setOutput Action for each inbetween value of the generic start and end.
      //   Only specific types are supported, but more can be added fairly easily.

      // NOTE (hitch) 11-28-17 Generic is the heaviest tween method!

      // JUSTIFICATION: Since adding it I've used this method more than anyother (hopefully Action
      //   will start to repalce it).

      easing = easing ?? Easing.EASE_LINEAR;
      float time = 0;
      T ret;

      while(time < length)
      {
        float t = easing(time / length);

        if(typeof(T) == typeof(Quaternion))
        {
          ret = (T)(object)Quaternion.SlerpUnclamped((Quaternion)(object)start,
                (Quaternion)(object)end, t);
        }
        else if(typeof(T) == typeof(Vector2))
        {
          ret = (T)(object)((1 - t) * (Vector2)(object)start + t * (Vector2)(object)end);
        }
        else if(typeof(T) == typeof(Vector3))
        {
          ret = (T)(object)((1 - t) * (Vector3)(object)start + t * (Vector3)(object)end);
        }
        else if(typeof(T) == typeof(Vector4))
        {
          ret = (T)(object)((1 - t) * (Vector4)(object)start + t * (Vector4)(object)end);
        }
        else if(typeof(T) == typeof(Color))
        {
          ret = (T)(object)((1 - t) * (Color)(object)start + t * (Color)(object)end);
        }
        else if(typeof(T) == typeof(float))
        {
          ret = (T)(object)((1 - t) * (float)(object)start + t * (float)(object)end);
        }
        else
        {
          // NOTE (hitch) 2-19-17 In C# 4.0 we could cast `start` and `end` to `dynamic` rather than
          // this ugly casting nightmare that only supports types explicitly set here:
          // - Some types might still need special handeling (ex. Quaternions).
          // - Also if we did this we could move from `ILerpable` to implementing * operator
          //   overloading! (NOTE ILerpable has been removed in favour of a generic action callback)
          // NOTE (hitch) 12-1-17 I have tried this with dynamics, it worked nicely, but probably
          //   had an unfortunate performace cost (but I did not notice). The problem is that
          //   Unity's .NET 4.5 implementation crashs occasionally. Once it is stable we could
          //   consider dynamics again. We would want to limit dynamic to use as a fallback, but we
          //   will need to test the perfomance (will it use some hacks and the CRL or actually
          //   include the dynamic runtime library). Ideally only use it as a temporary method
          //   before adding a proper fork for that type!
          Debug.LogError("Generic Tween does not work on type: " + typeof(T).ToString());
          setOutput(end);
          if(callbackEnding != null)
          {
            callbackEnding();
          }
          yield break;
        }

        setOutput(ret);
        yield return null;
        time += Time.deltaTime;
      }

      setOutput(end);
      if(callbackEnding != null)
      {
        callbackEnding();
      }
    }
  }


  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public static class Easing
  {

    // NOTE (hitch) 11-28-17 You can use an AnimationCurve.Evaluate in place of an EaseMethod for a
    //   custom timing function (Evaluate is compatable with this delegate).
    public delegate float EaseMethod(float f);

    public static readonly EaseMethod EASE_LINEAR       = (t => t);
    public static readonly EaseMethod EASE_QUAD_IN      = (t => t*t);
    public static readonly EaseMethod EASE_QUAD_OUT     = (t => t * (2-t));
    public static readonly EaseMethod EASE_QUAD_IN_OUT  = EaseInOut(EASE_QUAD_IN, EASE_QUAD_OUT);
    public static readonly EaseMethod EASE_CUBIC_IN     = (t => t*t*t);
    public static readonly EaseMethod EASE_CUBIC_OUT    = (t => { --t; return (t*t*t + 1); });
    public static readonly EaseMethod EASE_CUBIC_IN_OUT = EaseInOut(EASE_CUBIC_IN, EASE_CUBIC_OUT);
    public static readonly EaseMethod EASE_SIN_IN       = (t => 1 - Mathf.Cos((Mathf.PI/2)*t));
    public static readonly EaseMethod EASE_SIN_OUT      = (t => Mathf.Sin((Mathf.PI/2)*t));
    public static readonly EaseMethod EASE_SIN_IN_OUT   = EaseInOut(EASE_SIN_IN, EASE_SIN_OUT);
    public static readonly EaseMethod EASE_EXP_IN       = (t => Mathf.Pow(2, 10*(t-1)));
    public static readonly EaseMethod EASE_EXP_OUT      = (t => -Mathf.Pow(2,-10*t) + 1 );
    public static readonly EaseMethod EASE_EXP_IN_OUT   = EaseInOut(EASE_EXP_IN, EASE_EXP_OUT);
    public static readonly EaseMethod EASE_CIRC_IN      = (t => 1 - Mathf.Sqrt(1 - t*t));
    public static readonly EaseMethod EASE_CIRC_OUT     = (t => {--t; return Mathf.Sqrt(1 - t*t);});
    public static readonly EaseMethod EASE_CIRC_IN_OUT  = EaseInOut(EASE_CIRC_IN, EASE_CIRC_OUT);
    public static readonly EaseMethod EASE_ELASTIC_OUT  = EaseElastic(3.33333333333f);
    public static readonly EaseMethod EASE_BOUNCE_OUT   = EaseBounce(3);

    public static EaseMethod EaseInvert(EaseMethod ease)
    {
      return (t => 1 - ease(1 - t));
    }

    public static EaseMethod EaseInOut(EaseMethod easeIn, EaseMethod easeOut, float midPoint = 0.5f)
    {
      return (t => ((t < midPoint) ?
            (midPoint * easeIn(t / midPoint)) :
            ((1 - midPoint) * easeOut((t-midPoint)/(1-midPoint)) + midPoint)));
    }

    public static EaseMethod EaseElastic(float bounces)
    {
      return (t => (Mathf.Pow(2, (-10*t)) * Mathf.Sin((t-1/(4*bounces))*(2*Mathf.PI)*bounces) + 1));
    }

    public static EaseMethod EaseBounce(int bounces)
    {
      float[] bounceValues = new float[bounces * 4];
      {
        float sum = 0;
        float shift = 0;
        float width = 1;
        for(int i = 0; i < bounces; ++i)
        {
          sum += width;
          width *= 1.5f;
        }
        width /= 1.5f;
        sum -= width * 0.5f;
        for(int i = 0; i < bounces; ++i)
        {
          float height = Mathf.Sqrt(shift);
          bounceValues[i * 4] = shift;
          bounceValues[i * 4 + 1] = 2 * sum / width;
          bounceValues[i * 4 + 1] = bounceValues[i * 4 + 1] * bounceValues[i * 4 + 1] * (1-height);
          bounceValues[i * 4 + 2] = height;
          bounceValues[i * 4 + 3] = shift - (width/sum * 0.5f);
          width /= 1.5f;
          shift += width * 1.25f / sum;
        }
        bounceValues[3] = 0;
      }
      return ( t => {
        for(int i = bounces - 1; i >= 0; --i)
        {
          if(t > bounceValues[i * 4 + 3])
          {
            t -= bounceValues[i * 4];
            return t * t * bounceValues[i * 4 + 1] + bounceValues[i * 4 + 2];
          }
        }
        return 0;
      });
    }

    // These are for the convience of allowing the Unity Editor to select any built in EaseMethod:

    public static EaseMethod EaseFromEnum(EaseEnum ease)
    {
      switch(ease)
      {
        case EaseEnum.LINEAR:       { return EASE_LINEAR;       }
        case EaseEnum.QUAD_IN:      { return EASE_QUAD_IN;      }
        case EaseEnum.QUAD_OUT:     { return EASE_QUAD_OUT;     }
        case EaseEnum.QUAD_IN_OUT:  { return EASE_QUAD_IN_OUT;  }
        case EaseEnum.CUBIC_IN:     { return EASE_CUBIC_IN;     }
        case EaseEnum.CUBIC_OUT:    { return EASE_CUBIC_OUT;    }
        case EaseEnum.CUBIC_IN_OUT: { return EASE_CUBIC_IN_OUT; }
        case EaseEnum.SIN_IN:       { return EASE_SIN_IN;       }
        case EaseEnum.SIN_OUT:      { return EASE_SIN_OUT;      }
        case EaseEnum.SIN_IN_OUT:   { return EASE_SIN_IN_OUT;   }
        case EaseEnum.EXP_IN:       { return EASE_EXP_IN;       }
        case EaseEnum.EXP_OUT:      { return EASE_EXP_OUT;      }
        case EaseEnum.EXP_IN_OUT:   { return EASE_EXP_IN_OUT;   }
        case EaseEnum.CIRC_IN:      { return EASE_CIRC_IN;      }
        case EaseEnum.CIRC_OUT:     { return EASE_CIRC_OUT;     }
        case EaseEnum.CIRC_IN_OUT:  { return EASE_CIRC_IN_OUT;  }
        case EaseEnum.ELASTIC_OUT:  { return EASE_ELASTIC_OUT;  }
        case EaseEnum.BOUNCE_OUT:   { return EASE_BOUNCE_OUT;   }
      }
      return EASE_LINEAR;
    }

    public enum EaseEnum
    {
      LINEAR, QUAD_IN, QUAD_OUT, QUAD_IN_OUT, CUBIC_IN, CUBIC_OUT, CUBIC_IN_OUT, SIN_IN, SIN_OUT,
      SIN_IN_OUT, EXP_IN, EXP_OUT, EXP_IN_OUT, CIRC_IN, CIRC_OUT, CIRC_IN_OUT, ELASTIC_OUT,
      BOUNCE_OUT
    }

  }

}
