public class Either<A, B> implements Functor<B, Either<?, ?>> {
    public final A left;
    public final B right;
    public final boolean isRight;

    private Either(B b, boolean unused){
        // This is a "clever hack" to be able to distinguish constructors,
        // based on their type signatures (since generic types are erased at
        // compiletime).
        this.left = null;
        this.right = b;
        this.isRight = true;
    }
    private Either(A a){
        this.left = a;
        this.right = null;
        this.isRight = false;
    }

    public static <A, B> Either<A, B> right(B b){
        return new Either<A, B>(b, true);
    }
    public static <A, B> Either<A, B> left(A a){
        return new Either<A, B>(a);
    }

    public Either<A, B> pure(B b){
        return right(b);
    }

    public <C> Either<A, C> fmap(Function<B, C> f){
        if(this.isRight) return right(f.apply(this.right));
        return new Either<A, C>(this.left);
    }

    public String toString(){
        if(isRight) return "Right " + this.right.toString();
        return "Left " + this.left.toString();
    }
}
