
public static class LinearValue {
    public static float exe(float x, float xStart, float xEnd, float yStart, float yEnd) {
        return (x - xStart) * (yEnd - yStart) / (xEnd - xStart) + yStart;
    }
    public static float exe(float x, float yStart, float yEnd, float length) {
        return x * (yEnd - yStart) / length + yStart;
    }
}
