
public static class Modulus {
    public static int mod(this int a, int b) {
        return ((a % b) + b) % b;
    }
}
