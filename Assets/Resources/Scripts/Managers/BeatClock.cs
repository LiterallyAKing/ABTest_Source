using UnityEngine;
public class BeatClock : MonoBehaviour
{
	/*

This is a class that sends events based on a BPM. It allows you to easily & accurately sync aspects of your game to the tempo (bpm) of your soundtrack. 
You'll want to use BeatClock to start playing your song, so that it's in sync. If you change songs or BPMs, you just have to call InitializeBPM(newBPM). 
I'll keep you posted if I make additional improvements to this.
*/

//    public static BeatClock Instance;

    void Awake()
    {
//        Instance = this;
//        if (BPM.Equals(0.0)) Debug.LogWarning("BPM not set! Please set the BPM in the BeatClock");
//        else 
			InitializeBPM(BPM);
    }

    public double BPM;
    public double samplerate;
    public double secondsPerMeasure;
    public double samplesPerMeasure;
    public int thirtysecondcount = 0;
    public int sixteenthcount = 0;
    public int eighthcount = 0;
    public int quartercount = 0;
    public int halfcount = 0;
    public int measurecount = 0;
    public double thirtySecondLength;
    public double sixteenthLength;
    public double eigthLength;
    public double quarterLength;
    public double halfLength;
    public double measureLength;
    public double nextThirtySecond;
    public double nextMeasure;
    public double nextSixteenth;
    public double nextEighth;
    public double nextQuarter;
    public double nextHalf;

    public class BeatArgs
    {
        public enum Value { ThirtySecond, Sixteenth, Eighth, Quarter, Half, Full, Measure };
        public Value BeatType;
        public int BeatCount;
        public double CurrentTime;
        public double BeatTime;
        public double NextBeatTime;

        public BeatArgs() { }
        public BeatArgs(Value beatType, int beatCount, double currentTime, double beatTime, double nextBeatTime)
        {
            BeatType = beatType;
            BeatCount = beatCount;
            CurrentTime = currentTime;
            BeatTime = beatTime;
            NextBeatTime = nextBeatTime;
        }
    }

    public delegate void BeatEvent(object sender, BeatArgs args);
    public event BeatEvent Beat;
    public event BeatEvent ThirtySecond;
    public event BeatEvent Sixteenth;
    public event BeatEvent Eighth;
    public event BeatEvent Quarter;
    public event BeatEvent Half;
    public event BeatEvent Measure;

    void InitializeBPM(double _BPM)
    {
        BPM = _BPM;
        secondsPerMeasure = (60 / BPM * 4);
        samplesPerMeasure = secondsPerMeasure * samplerate;
        double startTime = AudioSettings.dspTime;
        FirstBeat(startTime);
    }

    void FirstBeat(double time)
    {
        measureLength = secondsPerMeasure;
        thirtySecondLength = measureLength / 32;
        sixteenthLength = measureLength / 16;
        eigthLength = measureLength / 8;
        quarterLength = measureLength / 4;
        halfLength = measureLength / 2;

        nextMeasure = time + measureLength;
        nextThirtySecond = time + thirtySecondLength;
        nextSixteenth = time + sixteenthLength;
        nextEighth = time + eigthLength;
        nextQuarter = time + quarterLength;
        nextHalf = time + halfLength;
    }

    void Update()
    {
        double currentTime = AudioSettings.dspTime;

        if (!(currentTime >= nextThirtySecond)) return;
        thirtysecondcount++;
        if (ThirtySecond != null)
            ThirtySecond(this, new BeatArgs(BeatArgs.Value.ThirtySecond, thirtysecondcount, currentTime, nextThirtySecond, nextThirtySecond + thirtySecondLength));
        if (Beat != null)
            Beat(this, new BeatArgs(BeatArgs.Value.ThirtySecond, thirtysecondcount, currentTime, nextThirtySecond, nextThirtySecond + thirtySecondLength));
        nextThirtySecond += thirtySecondLength;

        if (thirtysecondcount % 2 == 0) return;
        sixteenthcount++;
        if (Sixteenth != null)
            Sixteenth(this, new BeatArgs(BeatArgs.Value.Sixteenth, sixteenthcount, currentTime, nextSixteenth, nextSixteenth + sixteenthLength));
        if (Beat != null)
            Beat(this, new BeatArgs(BeatArgs.Value.Sixteenth, sixteenthcount, currentTime, nextSixteenth, nextSixteenth + sixteenthLength));
        nextSixteenth += sixteenthLength;

        if (sixteenthcount % 2 == 0) return;
        eighthcount++;
        if (Eighth != null)
            Eighth(this, new BeatArgs(BeatArgs.Value.Eighth, eighthcount, currentTime, nextEighth, nextEighth + eigthLength));
        if (Beat != null)
            Beat(this, new BeatArgs(BeatArgs.Value.Eighth, eighthcount, currentTime, nextEighth, nextEighth + eigthLength));
        nextEighth += eigthLength;

        if (eighthcount % 2 == 0) return;
        quartercount++;
        if (Quarter != null)
            Quarter(this, new BeatArgs(BeatArgs.Value.Quarter, quartercount, currentTime, nextQuarter, nextQuarter + quarterLength));
        if (Beat != null)
            Beat(this, new BeatArgs(BeatArgs.Value.Quarter, quartercount, currentTime, nextQuarter, nextQuarter + quarterLength));
        nextQuarter += quarterLength;

        if (quartercount % 2 == 0) return;
        halfcount++;
        if (Half != null)
            Half(this, new BeatArgs(BeatArgs.Value.Half, halfcount, currentTime, nextHalf, nextHalf + halfLength));
        if (Beat != null)
            Beat(this, new BeatArgs(BeatArgs.Value.Half, halfcount, currentTime, nextHalf, nextHalf + halfLength));
        nextHalf += halfLength;

        if (halfcount % 2 == 0) return;
        measurecount++;
        if (Measure != null)
            Measure(this, new BeatArgs(BeatArgs.Value.Measure, measurecount, currentTime, nextMeasure, nextMeasure + measureLength));
        if (Beat != null)
            Beat(this, new BeatArgs(BeatArgs.Value.Measure, measurecount, currentTime, nextMeasure, nextMeasure + measureLength));
        nextMeasure += measureLength;
    }
}

