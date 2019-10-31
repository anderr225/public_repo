(ns lab2.core)

(defn get_trapezoid_area
  [f l r]
  (* (- r l)(/ (+ (f l) (f r)) 2)))
  
(defn integrate [f] (
  let [
    inner (memoize (get_recursive_integration f))
  ] 
  (
    partial inner inner
  )
))

(defn get_recursive_integration [f] (
    fn [memo_f x] (
      let [h 0.1 inner_i (fn [x] (memo_f memo_f x))] 
        (
          let[next_v (- x 1) a (* x next_v) b (* x h)] (
            (print "(")
            (if (= x 1) 
              (get_trapezoid_area f 0 x)
              (+ ((get_trapezoid_area f a b)) (inner_i next_v)))
          )
        )
    )
))

(defn make_integral [f]
  (let [integrate-n (integrate f)]
    (fn [x]
      (let[
        n (int (/ x h))
        border (* h n)
      ]
        (+ (integrate-n n) 
        (get_trapezoid_area f border x))
      )
    )
  )
)

(def foo (make_integral (fn [x] (* x x))))

(print (foo 10))
(print (foo 10))
