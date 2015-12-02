public interface Monad<A, M extends Monad<?,?>> {
    <B> M bind(Function<A, B> f);
    M pure(A a);
}
