
public class TokenError : Token {
    public string Value { get; private set; }

    public TokenError(string value) {
        this.Value = value;
    }
}