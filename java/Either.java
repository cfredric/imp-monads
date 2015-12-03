public abstract class Either<A, B> {
    public boolean isRight;

    public abstract <C> Either<A, C> fmap(Function<B, C> f);
}

class Right<A, B> extends Either<A, B> {
    public B val;
    public Right(B b){
        val = b;
        isRight = true;
    }

    public <C> Either<A, C> fmap(Function<B, C> f){
        return new Right<A, C>(f.apply(val));
    }

    public <C> Either<A, C> bind(Function<B, Either<A, C>> f){
        return f.apply(val);
    }
}

class Left<A, B> extends Either<A, B> {
    public final A val;
    public Left(A a){
        val = a;
        isRight = false;
    }

    public <C> Either<A, C> fmap(Function<B, C> f){
        return new Left<A, C>(val);
    }

    public <C> Either<A, C> bind(Function<B, Either<A, C>> f){
        return new Left<A, C>(val);
    }
}
