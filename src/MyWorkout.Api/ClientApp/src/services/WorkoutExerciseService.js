export async function getAllWorkoutExercisesByWorkoutIdAsync(workoutId) {
  const response = await fetch(`/api/workoutexercise/workout/${workoutId}`);
  return await response.json();
}

export async function updateWorkoutExerciseAsync(workoutExercise) {
  const response = await fetch(`/api/workoutexercise/${workoutExercise.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(workoutExercise)
  })
  return await response.json();
}
