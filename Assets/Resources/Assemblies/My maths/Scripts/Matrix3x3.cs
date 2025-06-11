using UnityEngine;

public struct Matrix3x3 {
    public float m00, m01, m02,
                 m10, m11, m12,
                 m20, m21, m22;


    public Matrix3x3(
        float m00, float m01, float m02,
        float m10, float m11, float m12,
        float m20, float m21, float m22
    ) {
        this.m00 = m00;
        this.m01 = m01;
        this.m02 = m02;
        this.m10 = m10;
        this.m11 = m11;
        this.m12 = m12;
        this.m20 = m20;
        this.m21 = m21;
        this.m22 = m22;
    }
    public static Vector3 operator *(Matrix3x3 m, Vector3 v) =>
        new Vector3(
            m.m00 * v.x + m.m01 * v.y + m.m02 * v.z,
            m.m10 * v.x + m.m11 * v.y + m.m12 * v.z,
            m.m20 * v.x + m.m21 * v.y + m.m22 * v.z
        );
    public static Matrix3x3 operator *(float x, Matrix3x3 m) =>
        new Matrix3x3(
            x * m.m00, x * m.m01, x * m.m02,
            x * m.m10, x * m.m11, x * m.m12,
            x * m.m20, x * m.m21, x * m.m22
        );
    public Matrix3x3 inverse() {
        float det(float a, float b, float c, float d) => a * d - b * c;
        float detM =
              m00 * det(m11, m12, m21, m22)
            - m01 * det(m10, m12, m20, m22)
            + m02 * det(m10, m11, m20, m21);
        Matrix3x3 adjugate = new Matrix3x3(
            +det(m11, m12, m21, m22), -det(m01, m02, m21, m22), +det(m01, m02, m11, m12),
            -det(m10, m12, m20, m22), +det(m00, m02, m20, m22), -det(m00, m02, m10, m12),
            +det(m10, m11, m20, m21), -det(m00, m01, m20, m21), +det(m00, m01, m10, m11)
        );
        return (1 / detM) * adjugate;
    }
}
