
using System;

public static class LinearValue {
    public static float exe(float x, float xStart, float xEnd, float yStart, float yEnd) {
        return (x - xStart) * (yEnd - yStart) / (xEnd - xStart) + yStart;
    }
    public static float exe(float x, float yStart, float yEnd, float length) {
        return x * (yEnd - yStart) / length + yStart;
    }
    public static Func<float, float> getFunc(float yStart, float yEnd, float length) {
        return (float x) => exe(x, yStart, yEnd, length);
    }
    public static Func<float, float> getFunc(float xStart, float xEnd, float yStart, float yEnd) {
        return (float x) => exe(x, xStart, xEnd, yStart, yEnd);
    }
}
