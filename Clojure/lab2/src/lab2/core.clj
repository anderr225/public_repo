(ns lab2.core)

(def h 0.1)

(defn get_trapezoid_area
  [f l r]
  (* (- r l)(/ (+ (f l) (f r)) 2)))
  
(defn get_recursive_integration [f] (
    fn [memo_f n] (
      let [inner_i (fn [n] (memo_f memo_f n))] 
        (
          let[next_v (- n 1) a (* h next_v) b (* n h)] (
            if (= n 1) 
              (get_trapezoid_area f 0 h)
              (+ (get_trapezoid_area f a b) (inner_i next_v))
          )
        )
    )
))

(defn get_memoized_integration_fn [f] (
  let [
    inner (memoize (get_recursive_integration f))
  ] 
  (
    partial inner inner
  )
))

(defn make_integral [f]
  (let [memoized_integration_fn (get_memoized_integration_fn f)]
    (fn [x]
      (let[
        n (int (/ x h))
        border (* h n)
      ]    
        (+ (memoized_integration_fn n) 
        (get_trapezoid_area f border x))
      )
    )
  )
)

;(def f (make_integral (fn [x] (* x x))))
;
;(println (f 5))
;(println (f 5))
