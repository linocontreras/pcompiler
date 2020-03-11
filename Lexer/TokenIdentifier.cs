
public class TokenIdentifier : Token {
    public string Value { get; private set; }

    public TokenIdentifier(string value) {
        this.Value = value;
    }
}