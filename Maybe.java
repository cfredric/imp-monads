public class Maybe<A> implements Functor<A, Maybe<?>> {
    public final A val;
    public final boolean isJust;

    public Maybe(A a){
        this.val = a;
        this.isJust = a == null;
    }

    public <B> Maybe<B> fmap(Function<A, B> f){
        if(this.val == null) return nothing();
        return just(f.apply(this.val));
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
        if(this.val == null) return "Nothing";
        return "Just " + this.val.toString();
    }
}
