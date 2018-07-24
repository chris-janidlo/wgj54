public enum ZDir { Positive, Negative }

public static class ZDirMethods {
	
	public static ZDir Flipped (this ZDir original) {
		if (original == ZDir.Positive) {
			return ZDir.Negative;
		}
		else {
			return ZDir.Positive;
		}
	}

	public static bool IsPositive (this ZDir query) {
		return query == ZDir.Positive;
	}

	public static bool IsNegative (this ZDir query) {
		return query == ZDir.Negative;
	}

}
