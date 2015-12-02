public class Maybe<A> implements Functor<A, Maybe<?>>, Monad<A, Maybe<?>> {
    public final A val;
    public final boolean isJust;

    public Maybe(A a){
        this.val = a;
        this.isJust = a != null;
    }

    public <B> Maybe<B> fmap(Function<A, B> f){
        if(this.isJust) return just(f.apply(this.val));
        return nothing();
    }

    @SuppressWarnings("unchecked")
    public <B> Maybe<B> bind(Function<A, Maybe<?>> f){
        if(this.isJust) return (Maybe<B>) f.apply(this.val);
        return nothing();
    }

    public Maybe<A> pure(A a){
        return just(a);
    }

    public static <A> Maybe<A> nothing(){
        return new Maybe<A>(null);
    }

    public static <A> Maybe<A> just(A a){
        return new Maybe<A>(a);
    }

    public String toString(){
        if(!this.isJust) return "Nothing";
        return "Just " + this.val.toString();
    }
}
