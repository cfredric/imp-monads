public class Maybe<A> implements Monad<A, Maybe<?>> {
    private final A a;

    public Maybe(A a){
        this.a = a;
    }

    public <B> Maybe<B> bind(Function<A, B> f){
        if(a == null) return nothing();
        return just(f.apply(a));
    }

    public Maybe<A> pure(A a){
        return just(a);
    }

    public static <A> Maybe<A> nothing(){
        return new Maybe(null);
    }

    public static <A> Maybe<A> just(A a){
        return new Maybe(a);
    }
}
