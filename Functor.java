public interface Functor <A, F extends Functor<?,?>> {
    <B> F fmap(Function<A, B> f);
    F pure(A a);
}
