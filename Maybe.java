public abstract class Maybe<A> {
    public boolean isJust;
    public abstract <B> Maybe<B> fmap(Function<A, B> f);
    public abstract <B> Maybe<B> bind(Function<A, Maybe<B>> f);
}

class Nothing<A> extends Maybe<A> {
    public Nothing(){
        isJust = false;
    }

    public <B> Maybe<B> fmap(Function<A, B> f){
        return new Nothing<B>();
    }
    @SuppressWarnings("unchecked")
    public <B> Maybe<B> bind(Function<A, Maybe<B>> f){
        return new Nothing<B>();
    }
}

class Just<A> extends Maybe<A> {
    public final A val;
    public Just(A a){
        val = a;
        isJust = true;
    }

    public <B> Maybe<B> fmap(Function<A, B> f){
        B b = f.apply(val);
        if(b == null) return new Nothing<B>();
        return new Just<B>(b);
    }
    @SuppressWarnings("unchecked")
    public <B> Maybe<B> bind(Function<A, Maybe<B>> f){
        return f.apply(val);
    }
}
